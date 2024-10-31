using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;
using SV21T1020793.Web.Models;

namespace SV21T1020793.Web.Controllers
{
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 30;
        private const string SHIPPER_SEARCH_CONDITION = "ShipperSearchCondition";
        public IActionResult Index()
        {
            PaginationSearchInput? condition = ApplicationContext.GetSessionData<PaginationSearchInput>(SHIPPER_SEARCH_CONDITION);
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
            var data = CommonDataService.ListOfShippers(out rowCount, condition.Page, condition.PageSize, condition.SearchValue ?? "");
            ShipperSearchResult model = new ShipperSearchResult()
            {
                Page = condition.Page,
                PageSize = condition.PageSize,
                SearchValue = condition.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };

            ApplicationContext.SetSessionData(SHIPPER_SEARCH_CONDITION, condition);

            return View(model);
        }

        public IActionResult Create()
        {
            @ViewBag.Title = "Bổ sung người giao hàng mới";
            var data = new Shipper()
            {
                ShipperId = 0
            };
            return View("Edit", data);
        }
        public IActionResult Edit(int Id = 0)
        {
            @ViewBag.Title = "Cập nhật thông tin người giao hàng";
            var data = CommonDataService.GetShipper(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
        public IActionResult Save(Shipper data)
        {
            if (data.ShipperId == 0)
                CommonDataService.AddShipper(data);
            else
                CommonDataService.UpdateShipper(data);
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int Id)
        {
            ViewBag.Title = "Xóa thông tin người giao hàng";
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteShipper(Id);
                return RedirectToAction("Index");
            }

            var data = CommonDataService.GetShipper(Id);
            if (data == null)
                return RedirectToAction("Index");
            return View(data);
        }
    }
}
