﻿using LiteCommerce.Web;
using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;
using SV21T1020793.Web.Models;

namespace SV21T1020793.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 30;
        private const string EMPLOYEE_SEARCH_CONDITION = "EmployeeSearchCondition";
        public IActionResult Index()
        {
            PaginationSearchInput? condition = ApplicationContext.GetSessionData<PaginationSearchInput>(EMPLOYEE_SEARCH_CONDITION);
            if (condition == null)
                condition = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            return View(condition);
        }

        public IActionResult Search(PaginationSearchInput condition)
        {
            int rowCount;
            var data = CommonDataService.ListOfEmployees(out rowCount, condition.Page, condition.PageSize, condition.SearchValue ?? "");
            EmployeeSearchResult model = new EmployeeSearchResult()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(EMPLOYEE_SEARCH_CONDITION, condition);

            return View(model);
        }

        public IActionResult Create()
        {
            @ViewBag.Title = "Bổ sung nhân viên mới";
            var data = new Employee()
            {
                EmployeeID = 0,
                IsWorking = true,
                Photo ="nophoto.png"
            };
            return View("Edit", data);
        }
        public IActionResult Edit(int Id = 0)
        {
            @ViewBag.Title = "Cập nhật thông tin nhân viên";
            var data = CommonDataService.GetEmployee(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
        [HttpPost]
        public IActionResult Save(Employee data, string _BirthDate, IFormFile? _Photo)
        {
            ViewBag.Title = data.EmployeeID == 0 ? "Bổ sung nhân viên mới" : "Cập nhật thông tin nhân viên";

            // Kiểm tra nếu dữ liệu đầu vào không hợp lệ thì tạo ra một thông báo lỗi và lưu trữ vào ModelState
            if (string.IsNullOrWhiteSpace(data.FullName))
                ModelState.AddModelError(nameof(data.FullName), "Họ tên nhân viên không được để trống");
                ModelState.AddModelError(nameof(data.Address), "Vui lòng nhập địa chỉ của nhân viên");
            if (string.IsNullOrWhiteSpace(data.Phone))
                ModelState.AddModelError(nameof(data.Phone), "Vui lòng nhập sđt của nhân viên");
            if (string.IsNullOrWhiteSpace(data.Email))
                ModelState.AddModelError(nameof(data.Email), "Vui lòng nhập email của nhân viên");
            // Kiểm tra ngày sinh
            DateTime? d = _BirthDate.ToDateTime();
            if (!d.HasValue)
                ModelState.AddModelError(nameof(data.BirthDate), "Ngày sinh không hợp lệ");
            else if (d.Value > DateTime.Today)
                ModelState.AddModelError(nameof(data.BirthDate), "Ngày sinh không được lớn hơn ngày hiện tại");
            else
                data.BirthDate = d.Value;

            // Kiểm tra ảnh
            if (_Photo == null)
            {
                ModelState.AddModelError(nameof(data.Photo), "Vui lòng chọn ảnh của nhân viên");
            }
            else
            {
                // Kiểm tra định dạng ảnh
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };
                var extension = Path.GetExtension(_Photo.FileName).ToLower();
                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(nameof(data.Photo), "Định dạng ảnh không hợp lệ. Chỉ chấp nhận các định dạng: .jpg, .jpeg, .png");
                }
                else if (ModelState.IsValid)
                {
                    string fileName = $"{DateTime.Now.Ticks}-{_Photo.FileName}";
                    string filePath = Path.Combine(ApplicationContext.WebRootPath, @"images\employees", fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        _Photo.CopyTo(stream);
                    }
                    data.Photo = fileName;
                }
            }

            if (data.EmployeeID == 0)
            {
                CommonDataService.AddEmployee(data);
            }
            else
            {
                CommonDataService.UpdateEmployee(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            ViewBag.Title = "Xóa thông tin nhân viên";
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteEmployee(Id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetEmployee(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}
