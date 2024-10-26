using Microsoft.AspNetCore.Mvc;

namespace SV21T1020793.Web.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            @ViewBag.Title = "Bổ sung người giao hàng mới";
            return View("Edit");
        }
        public IActionResult Edit(int Id = 0)
        {
            ViewBag.Title = "Cập nhật thông tin mặt hàng";
            return View();
        }
        public IActionResult Delete(int Id)
        {
            ViewBag.Title = "Xóa mặt hàng";
            return View();
        }
        public IActionResult Photo(int id = 0, string method = "", int photoId = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung ảnh cho mặt hàng";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi ảnh cho mặt hàng";
                    return View();
                case "delete":
                    //TODO: Xóa ảnh (xóa trực tiếp, không cần confirm)
                    return RedirectToAction("Edit", new { id = id });
                default:
                    return RedirectToAction("Index");
            }
        }
        public IActionResult Attribute(int id = 0, string method = "", int attributeId = 0)
        {
            switch (method)
            {
                case "add":
                    ViewBag.Title = "Bổ sung thuộc tính cho mặt hàng";
                    return View();
                case "edit":
                    ViewBag.Title = "Thay đổi thuoovj tính của mặt hàng";
                    return View();
                case "delete":
                    //TODO: Xóa thuộc tính (xóa trực tiếp, không caanff confirm)
                    return RedirectToAction("Edit", new { id = id });
                default:
                    return RedirectToAction("Index");
            }
        }
    }
}
