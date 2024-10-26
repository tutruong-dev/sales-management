using Microsoft.AspNetCore.Mvc;
using SV21T1020793.BusinessLayers;
using SV21T1020793.DomainModels;

namespace SV21T1020793.Web.Controllers
{
    public class ShipperController : Controller
    {
        public const int PAGE_SIZE = 5;
        public IActionResult Index(int page = 1, string searchValue = "")
        {
            int rowCount;
            var data = CommonDataService.ListOfShippers(out rowCount, page, PAGE_SIZE, searchValue ?? "");

            int pageCount = rowCount / PAGE_SIZE;
            if (rowCount % PAGE_SIZE > 0)
                pageCount += 1;

            ViewBag.Page = page;
            ViewBag.rowCount = rowCount;
            ViewBag.PageCount = pageCount;
            ViewBag.SearchValue = searchValue;

            return View(data);
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
