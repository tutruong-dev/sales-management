using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;
using SV21T1020793.Web.Models;

namespace SV21T1020793.Web.Controllers
{
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 30;
        private const string CATEGORY_SEARCH_CONDITION = "CategorySearchCondition";
        public IActionResult Index()
        {
            PaginationSearchInput? condition = ApplicationContext.GetSessionData<PaginationSearchInput>(CATEGORY_SEARCH_CONDITION);
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
            var data = CommonDataService.ListOfCategories(out rowCount, condition.Page, condition.PageSize, condition.SearchValue ?? "");
            CategorySearchResult model = new CategorySearchResult()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(CATEGORY_SEARCH_CONDITION, condition);

            return View(model);
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