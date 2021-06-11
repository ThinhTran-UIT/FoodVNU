using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;
using PagedList;
namespace BanHang.Controllers
{
    public class TimKiemController : Controller
    {
        // GET: TimKiem
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [HttpGet]
        public ActionResult KQTimKiem(string TuKhoa,int? page)
        {
            var list = db.NhomSanPhams;
            ViewBag.list = list;
            if(Request.HttpMethod!="GET")
            {
                page = 1;
            }
            ViewBag.TuKhoa = TuKhoa;
            int l = 9;
            int m=(page??1);
            var listSP = db.Products.Where(n => n.Name.Contains(TuKhoa));
            return View(listSP.OrderBy(n=>n.Name).ToPagedList(m,l));
        }
        [HttpPost]
        public ActionResult LayTuKhoa(string TuKhoa)
        {
            
            return RedirectToAction("KQTimKiem",new { @TuKhoa = TuKhoa });
        }
    }
}