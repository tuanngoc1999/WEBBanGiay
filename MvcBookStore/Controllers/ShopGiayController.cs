using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcBookStore.Models;

namespace MvcBookStore.Controllers
{
    public class ShopGiayController : Controller
    {
        dbShopGiayDataContextDataContext data = new dbShopGiayDataContextDataContext();
        //dbShopGiayContext data = new dbShopGiayContext();
        // GET: BookStore

        private List<GIAY> LayGIAYmoi(int count)
        {
            return data.GIAYs.OrderByDescending(a => a.Ngaycapnhat).Take(count).ToList();
        }
        public ActionResult Index()
        {
            var GIAYmoi = LayGIAYmoi(5);

            return View(GIAYmoi);
        }
        public ActionResult NHASANXUAT()
        {
            var nsx = from cd in data.NHASANXUATs select cd;
            return PartialView(nsx);
        }
        public ActionResult LoaiGiay()
        {
            var LoaiGiay = from cd in data.LOAIGIAYs select cd;
            return PartialView(LoaiGiay);
        }
        public ActionResult SPTheoLoaiGiay(int id)
        {
            var GIAY = from cd in data.GIAYs where cd.MaGiay == id select cd;
            return View(GIAY);
        }
        public ActionResult SPTheoNSX(int id)
        {
            var GIAY = from cd in data.GIAYs where cd.MaNSX == id select cd;
            return View(GIAY);
        }
        public ActionResult Details(int id)
        {
            var GIAY = from s in data.GIAYs
                       where s.MaGiay == id
                       select s;
            return View(GIAY.Single());
        }
    }
}