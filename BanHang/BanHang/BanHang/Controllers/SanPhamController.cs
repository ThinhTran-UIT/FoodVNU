using BanHang.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace BanHang.Controllers
{
    public class SanPhamController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        //GET: SanPham
        public ActionResult SanPham1()
        {
            var lstSanPhamCom = db.Products.Where(n => n.IdNCC == 1);
            ViewBag.listSP = lstSanPhamCom;

            var lstSanPhamLau = db.Products.Where(n => n.IdLSP == 2);
            ViewBag.listLau = lstSanPhamLau;

            return View();
        }
        public ActionResult SanPham2()
        {
            return View();
        }
        [ChildActionOnly]
        public ActionResult SanPhamPartial()
        {
            //var lstSanPhamCom = db.Products.Where(n => n.IdLSP == 1);
            return PartialView();
        }
        [ChildActionOnly]
        public ActionResult category()
        {
            return PartialView();
        }


        public ActionResult SanPham(int? IdLSP,int? NhomSP,int? page)
        {
            var list = db.NhomSanPhams;
            ViewBag.list = list;
            int pagesize = 6;
            int pagenum = (page ?? 1);
            if (NhomSP == null || IdLSP==null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            if(IdLSP==0)
            {

                ViewBag.IdLSP = IdLSP;
                ViewBag.NhomSP = NhomSP;
                var listSP1 = db.Products.Where(n =>  n.NhomSP == NhomSP);
                return View(listSP1.OrderBy(n => n.Id).ToPagedList(pagenum, pagesize));
            }
            var listSP = db.Products.Where(n => n.IdLSP == IdLSP && n.NhomSP == NhomSP);
            ViewBag.IdLSP = IdLSP;
            ViewBag.NhomSP = NhomSP;
            return View(listSP.OrderBy(n=>n.Id).ToPagedList(pagenum,pagesize));
        }
        public ActionResult Product_Info(int? Id)
        {
            if (Id == null )
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var sp = db.Products.Where(n => n.Id == Id);
            return View(sp);
        }
    }
}