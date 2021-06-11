using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;

namespace BanHang.Controllers
{
    public class KhachHangController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        // GET: KhachHang
        public ActionResult Index()
        {
            var lstkh = db.KhachHangs;
            return View(lstkh);
        }
        public ActionResult TruyVanDoiTuong()
        {
            var lstkh = from kh in db.KhachHangs where kh.IdKH==1 select kh;
            KhachHang Khang = db.KhachHangs.SingleOrDefault(n=>n.IdKH==1);
            return View(Khang);
        }
        public ActionResult SortDuLieu()
        {
            List<KhachHang> lstKH = db.KhachHangs.OrderByDescending(n => n.Name).ToList();
            return View(lstKH);
        }
        public ActionResult GroupByDuLieu()
        {
            List<ThanhVien> lstKH = db.ThanhViens.OrderByDescending(n => n.UserName).ToList();
            return View(lstKH);
        }

    }
}