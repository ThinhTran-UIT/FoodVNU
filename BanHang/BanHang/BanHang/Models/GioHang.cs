using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BanHang.Models
{
    public class GioHang
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int  SL { get; set; }
        public decimal Price { get; set; }
        public decimal Total { get; set; }
        public string Image { get; set; }

        public GioHang(int Id, int SL)
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.Id = Id;
                Product sp = db.Products.Single(n => n.Id == Id);
                this.Name = sp.Name;
                this.Image = sp.Image;
                this.SL = SL;
                this.Price = (decimal)sp.Price;
                this.Total = Price * SL;
            }

        }
        public GioHang(int Id)
        {
            using (QuanLyBanHangEntities db = new QuanLyBanHangEntities())
            {
                this.Id = Id;
                Product sp = db.Products.Single(n => n.Id == Id);
                this.Name = sp.Name;
                this.Image = sp.Image;
                this.Price = (decimal)sp.Price.Value;
                this.SL = 1;
                this.Total = Price * SL;
            }
        }

    }
}