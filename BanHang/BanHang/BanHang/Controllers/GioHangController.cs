using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;
namespace BanHang.Controllers
{
    public class GioHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if(listGH==null)
            {
                listGH = new List<GioHang>();
                Session["GioHang"] = listGH;
            }
            return listGH;
        }
        public ActionResult ThemGioHang(int Id,string strurl)
        {
            Product sp = db.Products.SingleOrDefault(n => n.Id == Id);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            List<GioHang> listGH = LayGioHang();
            GioHang spcheck = listGH.SingleOrDefault(n => n.Id == Id);
            if(spcheck!=null)
            {
                spcheck.SL++;
                spcheck.Total = spcheck.SL * spcheck.Price;
                return Redirect(strurl);
            }
            GioHang itemGH = new GioHang(Id);
            listGH.Add(itemGH);
            return Redirect(strurl);
        }
        public double TongSl()
        {
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if(listGH==null)
            {
                return 0;
            }
            return listGH.Sum(n => n.SL);
        }
        public decimal TongTien()
        {
            List<GioHang> listGH = Session["GioHang"] as List<GioHang>;
            if (listGH == null)
            {
                return 0;
            }
            return listGH.Sum(n => n.Total);
        }
        // GET: GioHang
        public ActionResult Index()
        {
            List<GioHang> listGH = LayGioHang();
            return View(listGH);
        }
        public ActionResult GioHangPartial()
        {
            ViewBag.TongSL = TongSl();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        [HttpGet]
        public ActionResult SuaGioHang(int Id)
        {
            if(Session["GioHang"]==null)
            {
                return RedirectToAction("Index");
            }
            List<GioHang> listGH = LayGioHang();
            GioHang spcheck = listGH.SingleOrDefault(n => n.Id == Id);
            if(spcheck==null)
            {
                return RedirectToAction("Index","Home");
            }
            ViewBag.listGioHang = listGH;
            return View(spcheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(FormCollection f)
        {
            int Id = int.Parse(f["IdSP"]);
            int SL = int.Parse(f["SLSP"]);
            List<GioHang> listGH = LayGioHang();
            GioHang itemGH = listGH.Find(n => n.Id == Id);
            itemGH.SL = SL;
            itemGH.Total = itemGH.Price * SL;
            return RedirectToAction("Index");
        }

        public ActionResult XoaGioHang(int Id)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index");
            }
            List<GioHang> listGH = LayGioHang();
            GioHang spcheck = listGH.SingleOrDefault(n => n.Id == Id);
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            listGH.Remove(spcheck);
            return RedirectToAction("Index");
        }
        public ActionResult DatHang(KhachHang kh)
        {
            KhachHang khang=new KhachHang();
            if(Session["TaiKhoan"]==null)
            {              
                return RedirectToAction("DangKy","Home");
            }
            else
            {
                ThanhVien tv=Session["TaiKhoan"] as ThanhVien;
                khang.Name = tv.Name;
                khang.Address = tv.Address;
                khang.Phone = tv.Phone;
                khang.Email = tv.Email;
                db.KhachHangs.Add(khang);
                db.SaveChanges();
            }
            Bill bill = new Bill();
            bill.IdKH = khang.IdKH;
            bill.Date_order = DateTime.Now;
            bill.Delivered = false;
            bill.Payed = false;
            bill.Interest = 0;
            db.Bills.Add(bill);
            db.SaveChanges();

            List<GioHang> listGH = LayGioHang();
            foreach (var item in listGH)
            {
                BillInfo billInfo = new BillInfo();
                billInfo.IdB = bill.Id;
                billInfo.IdSP = item.Id;
                billInfo.Number = item.SL;
                billInfo.Price = item.Price;
                db.BillInfoes.Add(billInfo);
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("Index");
        }
    }
}