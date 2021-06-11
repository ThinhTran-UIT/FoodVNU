using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;

namespace BanHang.Controllers
{
    public class AjaxController : Controller
    {
        // GET: AJAX
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Ajax()
        {
            return View();
        }

        public ActionResult LoadAjaxBeginForm(FormCollection f)
        {
            string KQ = f["txt1"].ToString();
            return Content(KQ);
        }

        public ActionResult LoadSPAjax()
        {
            var lstSanPham = db.Products;
            return PartialView("LoadSPAjax",lstSanPham);
        }
    }
}