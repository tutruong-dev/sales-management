using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;

namespace SV21T1020793.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 5;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount;
            var data = CommonDataService.ListOfCategories(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0)
                pageCount += 1;

            ViewBag.Page = page;
            ViewBag.rowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);      //List<Customer>        IEnumerable<Customer>
        }
        public IActionResult Create()
        {
            @ViewBag.Title = "Bổ sung loại hàng mới";
            var data = new Category()
            {
                CategoryId = 0,
            };
            return View("Edit", data);
        }
        public IActionResult Edit(int Id = 0)
        {
            ViewBag.Title = "Chỉnh sửa thông tin loại hàng";
            var data = CommonDataService.GetCategory(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
        public IActionResult Save(Category data)
        {
            //TODO: Kiểm tra dữ liệu đầu vào

            if (data.CategoryId == 0)
            {
                CommonDataService.AddCategory(data);
            }
            else
            {
                CommonDataService.UpdateCategory(data);
            }
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            ViewBag.Title = "Xóa thông tin loại hàng";
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCategory(Id);
                return RedirectToAction("Index");
            }
            var data = CommonDataService.GetCategory(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}