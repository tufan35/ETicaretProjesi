using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ETicaretProjesi.Models
{
   

    public class Urunler
    {
        public static void HighlightsAdd(int id)
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {

                Products prod = db.Products.FirstOrDefault(p => p.ProductID == id);
                prod.Highlights = prod.Highlights + 1;
                db.SaveChanges();
            }

        }

        public static List<vw_aktif_urunler> SliderUrunlerGetir()
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.Where(p => p.Status == 2).ToList();
                return list;
            }

        }
        public static List<vw_aktif_urunler> EnYeniUrunlerGetir(string parametre, string sayfano)
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = null;

                if (parametre == "anasayfa")
                {

                     list = db.vw_aktif_urunler.Take(8).OrderBy(p => p.AddDate).ToList();
                    return list;
                }
                else if (parametre == "enyeniurunler")
                {
                    list = db.vw_aktif_urunler.Take(4).OrderBy(p => p.AddDate).ToList();
                    return list;
                }
                else
                {
                    int index = Convert.ToInt32(sayfano) * 4; 
                    list = db.vw_aktif_urunler.OrderByDescending( p => p.AddDate).Skip(index).Take(4).ToList();
                    return list;

                }
                return list;

            }

        }
        public static List<vw_aktif_urunler> OzelUrunlerGetir(string parametre, string sayfano)
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            { 
                List<vw_aktif_urunler> list = null;

                if (parametre =="anasayfa")
                {
                   list = db.vw_aktif_urunler.Where(p => p.Status == 3).Take(8).ToList();
                    return list;
                }
                else if (parametre == "ozelurunler")
                {
                    list = db.vw_aktif_urunler.Where( p => p.Status ==3).Take(4).ToList();

                }
                else
                {
                    int index = Convert.ToInt32(sayfano) * 4;
                    list = db.vw_aktif_urunler.Where(p => p.Status == 3).OrderBy(p => p.ProductName).Skip(index).Take(4).ToList();
                    return list;
                }
                return list;
            }

        }
        public static List<vw_aktif_urunler> IndirimliUrunlerGetir(string parametre, string sayfano)
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = null;
                if (parametre == "anasayfa")
                {
                    list = db.vw_aktif_urunler.OrderByDescending(p => p.Discount).Take(8).ToList();
                    return list;
                }else if (parametre == "indirimliurunler")
                {
                    list = db.vw_aktif_urunler.OrderByDescending(p => p.Discount).Take(4).ToList();
                    return list;
                }
                else
                {
                    int index = Convert.ToInt32(sayfano) * 4;
                    list = db.vw_aktif_urunler.OrderByDescending(p => p.Discount).Skip(index).Take(4).ToList();
                    return list;
                }
                return list;  
            }

        }
        public static List<vw_aktif_urunler> OneCikanUrunlerGetir()
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.OrderByDescending(p => p.Highlights).Take(8).ToList();
                return list;
            }

        }
        public static List<vw_aktif_urunler> CokSatanlarGetir()
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.OrderByDescending(p => p.BestSellers).Take(8).ToList();
                return list;
            }

        }
        public static List<vw_aktif_urunler> SuperUrunlerGetir()
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.OrderBy(p => p.Price).ToList();
                return list;
            }

        }
        public static vw_aktif_urunler GununUrununuGetir()
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                vw_aktif_urunler list = db.vw_aktif_urunler.FirstOrDefault(p => p.Status == 1);
                return list;
            }

        }
        public static List<vw_aktif_urunler> MarkalıUrunGetir(int id )
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.Where(p => p.SupplierID == id).ToList();
                return list;
            }

        }
        public static List<vw_aktif_urunler> KategoriliUrunGetir(int id)
        {
            using (ETicaretDBEntities db = new ETicaretDBEntities())
            {
                List<vw_aktif_urunler> list = db.vw_aktif_urunler.Where(p => p.CategoryID == id).ToList();
                return list;
            }

        }
    }





}