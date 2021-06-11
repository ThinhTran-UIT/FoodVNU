using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
namespace BanHang.Controllers
{
    public class HomeController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: Home
        public ActionResult Index()
        {
            var listcom = db.Products.Where(n => n.IdLSP == 1);
            ViewBag.listComMoi = listcom;
            var listlau = db.Products.Where(n => n.IdLSP == 2);
            ViewBag.listLauMoi = listlau;
            var listbanh = db.Products.Where(n => n.IdLSP == 3);
            ViewBag.listBanhMoi = listbanh;
            var listSP = db.Products;
            return View(listSP);
        }

        public ActionResult Checkout()
        {
            return View();
        }
        public ActionResult MenuPartial()
        {
            var listSP = db.NhomSanPhams;
            return PartialView(listSP);   
        }

        [HttpGet]
        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Dangky(ThanhVien f)
        {
            if(this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                    ViewBag.Thongbao = "Valid";
                    db.ThanhViens.Add(f);
                    db.SaveChanges();
                    return View();
                }
            }
            ViewBag.Thongbao = "Invalid";
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string username = f["username1"].ToString();
            string password = f["password1"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.UserName == username && n.Password == password);
            if(tv!=null)
            {
                Session["TaiKhoan"] = tv;
                return Content("<script>window.location.reload();</script>");

            }
            return Content("Username or password is wrong");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }

    }
}