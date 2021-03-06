﻿using MvcBookStore.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//using System.ComponentModel.DataAnnotations;

namespace MvcBookStore.Controllers
{
    public class NguoidungController : Controller
    {
        public class ExternalLoginConfirmationViewModel
        {
            [Required]
            [Display(Name = "TenDN")]
            public string TenDN { get; set; }
        }
        // GET: Nguoidung
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dangky()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangky(FormCollection collection, KHACHHANG kh)
        {
            dbShopGiayDataContextDataContext db = new dbShopGiayDataContextDataContext();
            var hoten = collection["HotenKH"];
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            var nhaplaimk = collection["NhaplaiMK"];
            var email = collection["Email"];
            var diachi = collection["Diachi"];
            var dienthoai = collection["Dienthoai"];
            var ngaysinh = String.Format("{0:MM/dd/yyyy}",collection["Ngaysinh"]);
            ADMIN ad = db.ADMINs.SingleOrDefault(n => n.ID == tendn);
            KHACHHANG khh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn);
            if (ad != null || khh != null)
                ViewData["Loi2"] = "Tên đăng nhập đã tồn tại";
            if (String.IsNullOrEmpty(hoten))
            {
                ViewData["Loi1"] = "Họ tên không được để trống";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi2"] = "Chưa nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
            {
                ViewData["Loi3"] = "Chưa nhập mật khẩu";
            }
            else if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi4"] = "Chưa xác nhận lại mật khẩu";
            }           
            else if (String.IsNullOrEmpty(email))
            {
                ViewData["Loi5"] = "Chưa nhập địa chỉ Email";
            }
            else if (String.IsNullOrEmpty(diachi))
            {
                ViewData["Loi6"] = "Chưa nhập địa chỉ của bạn";
            }
            else if (String.IsNullOrEmpty(dienthoai))
            {
                ViewData["Loi7"] = "Số điện thoại không được bỏ trống";
            }
            else
            {
                kh.HoTen = hoten;
                kh.Taikhoan = tendn;
                kh.Matkhau = matkhau;
                kh.Email = email;
                kh.DiachiKH = diachi;
                kh.DienthoaiKH = dienthoai;
                kh.Ngaysinh =DateTime.Parse(ngaysinh);
                db.KHACHHANGs.InsertOnSubmit(kh);
                db.SubmitChanges();
                return RedirectToAction("Dangnhap");
            }
            return this.Dangky();
        }

        public ActionResult Dangnhap()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Dangnhap(FormCollection collection, string returnUrl)
        {
            dbShopGiayDataContextDataContext db = new dbShopGiayDataContextDataContext();
            var tendn = collection["TenDN"];
            var matkhau = collection["Matkhau"];
            if (String.IsNullOrEmpty(tendn))
            {
                ViewData["Loi1"] = "Phải nhập tên đăng nhập";
            }
            else if (String.IsNullOrEmpty(matkhau))
                {
                ViewData["Loi2"] = "Phải nhập mật khẩu";
                }
                else
                {
                    KHACHHANG kh = db.KHACHHANGs.SingleOrDefault(n => n.Taikhoan == tendn && n.Matkhau == matkhau);
                ADMIN ad = db.ADMINs.SingleOrDefault(n => n.ID == tendn && n.pwd == matkhau);
                    if (kh != null)
                    {
                        ViewBag.Thongbao = "Đăng nhập thành công";
                        Session["Taikhoan"] = kh;
                        return RedirectToAction("Index", "ShopGiay");

                    }
                    else if(ad != null)
                    {
                    ViewBag.Thongbao = "Đăng nhập thành công";
                    Session["Taikhoan"] = kh;
                    return RedirectToAction("Giay", "Admin");
                }
                    else
                        ViewBag.Thongbao = "Tên đăng nhập hoặc mật khẩu không đúng";
                }
            return View();
        }
    }
}