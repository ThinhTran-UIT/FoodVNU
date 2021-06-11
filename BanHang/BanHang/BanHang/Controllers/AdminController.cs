using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BanHang.Models;

namespace BanHang.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            return View(db.Products);
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            ViewBag.NhomSP = new SelectList(db.NhomSanPhams.OrderBy(n => n.Id), "Id", "Name");
            ViewBag.IdLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.Id), "Id", "Name");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Product sp, HttpPostedFileBase Image)
        {
            ViewBag.NhomSP = new SelectList(db.NhomSanPhams.OrderBy(n => n.Id), "Id", "Name");
            ViewBag.IdLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.Id), "Id", "Name");
            if(Image.ContentLength>0)
            {
                var filename = Path.GetFileName(Image.FileName);
                    var path = Path.Combine(Server.MapPath("~/Content/img/HinhSP"), filename);
                if(System.IO.File.Exists(path))
                {
                    ViewBag.upload = "Image Exists";
                    return View();
                }
                else
                {
                    Image.SaveAs(path);
                    sp.Image = filename;

                }

            }
            db.Products.Add(sp);
            db.SaveChanges();
            return RedirectToAction("MProducts");
        }

        public ActionResult MProducts()
        {
            var listSP = db.Products;
            return View(listSP);
        }

        [HttpGet]
        public ActionResult ChinhSua(int Id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.Id == Id);
            ViewBag.NhomSP = new SelectList(db.NhomSanPhams.OrderBy(n => n.Id), "Id", "Name",sp.NhomSP);
            ViewBag.IdLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.Id), "Id", "Name",sp.IdLSP);

            return View(sp);
        }
                [ValidateInput(false)]
       [HttpPost]
       public ActionResult ChinhSua(Product sp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MProducts");
            }
            return View("MProducts");
        }

        [HttpGet]
        public ActionResult Xoa(int? Id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.Id == Id);
            ViewBag.NhomSP = new SelectList(db.NhomSanPhams.OrderBy(n => n.Id), "Id", "Name", sp.NhomSP);
            ViewBag.IdLSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.Id), "Id", "Name", sp.IdLSP);

            return View(sp);
        }
        [HttpPost] 
        public ActionResult Xoa(int Id)
        {
            Product sp = db.Products.SingleOrDefault(n => n.Id == Id);
            db.Products.Remove(sp);
            db.SaveChanges();

            return View("Index");
        }
    }
}