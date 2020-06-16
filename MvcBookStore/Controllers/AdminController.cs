using MvcBookStore.Models;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcBookStore.Controllers
{
    public class AdminController : Controller
    {
        dbShopGiayDataContextDataContext db = new dbShopGiayDataContextDataContext();
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Giay()
        {
            return View(db.GIAYs.ToList());
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(FormCollection collection)
        {
            var tendn = collection["email"];
            var matkhau = collection["password"];
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.ID == tendn && n.pwd == matkhau);
            //if (String.IsNullOrEmpty(tendn))
            //{
            //    ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            //}
            //else if (String.IsNullOrEmpty(matkhau))
            //{
            //    ViewData["Loi2"] = "Phải nhập mật khẩu";
            //}
            if (ad != null)
            {
                Session["AdminAccout"] = ad;
                return RedirectToAction("Index", "Admin");
            }
            else ViewBag.Thongbao = "Email hoặc mật khẩu không đúng!";
            return View();
        }
        [HttpGet]
        public ActionResult ThemmoiGiay()
        {
            ViewBag.MaLoai = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
        [HttpPost]
        public ActionResult ThemmoiGiay(GIAY giay, HttpPostedFileBase fileUpload)
        {
            var fileName = Path.GetFileName(fileUpload.FileName);
            var path = Path.Combine(Server.MapPath("~/Hinhsanpham"), fileName);
            if (System.IO.File.Exists(path))
            {
                ViewBag.Thongbao = "Hình ảnh đã tồn tại";
            }
            else fileUpload.SaveAs(path);
            ViewBag.MaLoai = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            return View();
        }
    }
}