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
            ViewBag.MaLoai = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if(fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/Hinhg"), fileName);
                    if (System.IO.File.Exists(path))
                    {
                        ViewBag.Thongbao = "Hình ảnh đã tồn tại";
                    }
                    else fileUpload.SaveAs(path);
                    giay.Anhbia = fileName;
                    db.GIAYs.InsertOnSubmit(giay);
                    db.SubmitChanges();
                }
                return RedirectToAction("Giay");
            }
            
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            GIAY giay = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            if (giay == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            ViewBag.MaLoai = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", giay.MaLoai);
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", giay.MaNSX);
            return View(giay);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GIAY giay, HttpPostedFileBase fileUpload)
        {
            ViewBag.MaLoai = new SelectList(db.LOAIGIAYs.ToList().OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NHASANXUATs.ToList().OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            if (fileUpload == null)
            {
                ViewBag.Thongbao = "Vui lòng chọn ảnh";
                return View();
            }
            else
            {
                if (ModelState.IsValid)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    var path = Path.Combine(Server.MapPath("~/fastfood/"), fileName);
                    if (System.IO.File.Exists(path))
                        ViewBag.Thongbao = "File đã tồn tại";
                    else
                    {
                        fileUpload.SaveAs(path);
                    }
                    giay.Anhbia = fileName;
                    UpdateModel(giay);
                    db.SubmitChanges();
                }
                return RedirectToAction("Sanpham");
            }
        }
        public ActionResult SPTheoLoaiGiay(int id)
        {
            var GIAY = from s in db.GIAYs where s.MaLoai == id select s;
            return View(GIAY);
        }
        public ActionResult SPTheoNSX(int id)
        {
            var GIAY = from cd in db.GIAYs where cd.MaNSX == id select cd;
            return View(GIAY);
        }
        public ActionResult DS_ThuongHieu()
        {

            return View(db.NHASANXUATs.ToList());
        }
        public ActionResult Details(int id)
        {
            var GIAY = from s in db.GIAYs
                       where s.MaGiay == id
                       select s;
            return View(GIAY.Single());
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            GIAY g = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            ViewBag.MaSP = g.MaGiay;
            if (g == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(g);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult Xacnhan(int id)
        {
            GIAY g = db.GIAYs.SingleOrDefault(n => n.MaGiay == id);
            ViewBag.MaSP = g.MaGiay;
            if (g == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            db.GIAYs.DeleteOnSubmit(g);
            db.SubmitChanges();
            return RedirectToAction("GIAY");
        }
    }
}