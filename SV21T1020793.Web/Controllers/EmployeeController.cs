using LiteCommerce.Web;
using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;

namespace SV21T1020793.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private const int PAGE_SIZE = 10;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount;
            var data = CommonDataService.ListOfEmployees(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0)
                pageCount += 1;

            ViewBag.Page = page;
            ViewBag.rowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);      //List<Employee>        IEnumerable<CEmployee>
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
            //TODO :Kiểm soát dữ liệu đầu vào

            //Xử lí cho ngày sinh
            DateTime? d = _BirthDate.ToDateTime();
            if (d.HasValue) //(d !=null)
                data.BirthDate = d.Value;

            //Xử lí với ảnh 
            if (_Photo != null)
            {
                string fileName = $"{DateTime.Now.Ticks}-{_Photo.FileName}";
                string filePath = Path.Combine(ApplicationContext.WebRootPath, @"images\employees", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    _Photo.CopyTo(stream);
                }
                data.Photo = fileName;
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
