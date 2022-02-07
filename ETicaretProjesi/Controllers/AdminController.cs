using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretProjesi.Models;

namespace ETicaretProjesi.Controllers
{
    public class AdminController : Controller
    {
        ETicaretDBEntities db = new ETicaretDBEntities();
        // GET: Admin
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Users users)
        {
            Users user = db.Users.FirstOrDefault(u => u.email == users.email && u.password == users.password);

            if (user != null)
            {
                Session["namesurname"] = user.namesurname;
                Session["UserID"] = user.UserID;
                return RedirectToAction("Anasayfa", "Admin");
            }

            return View();
        }
        public ActionResult logout()
        {
            Session.Abandon();
            Session.Remove("UserID");
            Session.Remove("namesurname");
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Anasayfa()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }



           
        }
        public ActionResult CategoryInsert()
        {
            if (Session["UserID"] != null)
            {
                List<Categories> catlist = db.Categories.Where(c => c.ParentID ==0).ToList();
                ViewData["catlist"] = catlist.Select(c => new SelectListItem { Text = c.CategoryName, Value = c.Categoryıd.ToString() }).ToList();

            }
            else
            {
                return RedirectToAction("Login");
            }


            return View();
        }
        [HttpPost] //post metodunda return view ile dönemezsin viewdata çektiğin için
        public ActionResult CategoryInsert(Categories cat)
        {

            cat.ParentID = cat.Categoryıd != 0 ? cat.Categoryıd : cat.ParentID = 0;
            cat.isActive = true;

            Categories ctgr = db.Categories.FirstOrDefault(c => c.CategoryName.ToLower() == cat.CategoryName.ToLower());
            if (ctgr == null)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                Session["Mesaj"] = "Kategori başarı ile kaydedildi.";
            }
            else
            {
                Session["Mesaj"] = "Kategori vardır.";
            }


            return RedirectToAction("CategoryInsert");
        }
        public ActionResult SupplierInsert()
        {
            if (Session["UserID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
         
        }//post metodunda return view ile dönemezsin viewdata çektiğin için
        [HttpPost]
        public ActionResult SupplierInsert(HttpPostedFileBase PhotoPath, Suppliers sup)
        {

            sup.IsActive = true;

            vw_suppliers spctg = db.vw_suppliers.FirstOrDefault(s => s.BrandName.ToLower() == sup.BrandName.ToLower());
            if (spctg == null)
            {
                sup.Photopath= PhotoPath.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/resimler"), Path.GetFileName(PhotoPath.FileName));
                PhotoPath.SaveAs(path);

                db.Suppliers.Add(sup);
                db.SaveChanges();
                Session["Mesaj"] = "Marka başarı ile kaydedildi.";
            }
            else
            {
                Session["Mesaj"] = "Marka vardır.";
            }


            return View();
        } 
        public ActionResult SupplierUpdateDelete()
        {
            List<Suppliers> urn = db.Suppliers.OrderBy(p => p.SupplierID).ToList();
            return View(urn);
        }
        public ActionResult SupplierUpdate(int id)
        {
           vw_suppliers urn = db.vw_suppliers.FirstOrDefault(p => p.SupplierID == id);
            return View(urn);
        }
        [HttpPost]
        public ActionResult SupplierUpdate(HttpPostedFileBase PhotoPath, Suppliers prod)
        {
            vw_suppliers prdt = db.vw_suppliers.FirstOrDefault(p => p.SupplierID == prod.SupplierID);
           if (PhotoPath != null)
            {
                prod.Photopath = PhotoPath.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/resimler"), Path.GetFileName(PhotoPath.FileName));
                PhotoPath.SaveAs(path);

            }
            else
            {

                prod.Photopath = prdt.Photopath;

            }
            prod.IsActive = prdt.IsActive;
            



            db.Entry(prod).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("SupplierUpdateDelete");
        }
        public ActionResult SupplierDelete(int id)
        {
            Suppliers prd = db.Suppliers.FirstOrDefault(p => p.SupplierID == id);
            db.Suppliers.Remove(prd);
            db.SaveChanges();

            return RedirectToAction("SupplierUpdateDelete");
        }
        public ActionResult ProductInsert()
        {
            if (Session["UserID"] != null)
            {
                List<Categories> catlist = db.Categories.ToList();

                ViewData["catlist"] = catlist.Select(c => new SelectListItem
                { Text = c.CategoryName, Value = c.Categoryıd.ToString() }).ToList();
                List<Suppliers> suplist = db.Suppliers.ToList();
                ViewData["suplist"] = suplist.Select(s => new SelectListItem
                { Text = s.BrandName, Value = s.SupplierID.ToString() }).ToList();

            }
            else
            {
                return RedirectToAction("Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ProductInsert(HttpPostedFileBase PhotoPath, Products prod)
        {
            Products prdctg = db.Products.FirstOrDefault(s => s.ProductName.ToLower() == prod.ProductName.ToLower());

            if (prdctg == null)
            {
                prod.AddDate = DateTime.Now;
                prod.IsActive = true;
                prod.Highlights = 0;
                prod.BestSellers = 0;
                prod.PhotoPath = PhotoPath.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/resimler"), Path.GetFileName(PhotoPath.FileName));
                PhotoPath.SaveAs(path);
                db.Products.Add(prod);
                db.SaveChanges();
                Session["Mesaj"] = "Ürün Kaydedilmiştir.";

            }
            else
            {
                Session["Mesaj"] = "Bu ürün zaten vardır.";
            }

            return RedirectToAction("ProductInsert");
        }
        public ActionResult ProductUpdateDelete()
        {
            List<vw_aktif_urunler> urn = db.vw_aktif_urunler.OrderBy(p => p.ProductID).ToList();
            return View(urn);
        }
        public ActionResult ProductUpdate(int id)
        {
            List<Categories> catlist = db.Categories.OrderBy(c => c.CategoryName).ToList();
            ViewData["catlist"] = catlist.Select(c => new SelectListItem
            { Text = c.CategoryName, Value = c.Categoryıd.ToString() }).ToList();


            List<Suppliers> suplist = db.Suppliers.OrderBy(c => c.BrandName).ToList();
            ViewData["suplist"] = suplist.Select(c => new SelectListItem
            { Text = c.BrandName, Value = c.SupplierID.ToString() }).ToList();

            vw_aktif_urunler urn = db.vw_aktif_urunler.FirstOrDefault(p => p.ProductID == id) ;

            return View(urn);
        }
        [HttpPost]
        public ActionResult ProductUpdate(HttpPostedFileBase PhotoPath, Products prod)
        {
            vw_aktif_urunler prdt = db.vw_aktif_urunler.FirstOrDefault(p => p.ProductID ==prod.ProductID);
            //Products prdt = db.Products.FirstOrDefault(p => p.ProductID == prod.ProductID);


            if (PhotoPath != null)
            {
                prod.PhotoPath = PhotoPath.FileName;
                string path = Path.Combine(Server.MapPath("~/Content/resimler"), Path.GetFileName(PhotoPath.FileName));
                PhotoPath.SaveAs(path);

            }
            else
            {

                  prod.PhotoPath = prdt.PhotoPath;

            }
            prod.IsActive = prdt.IsActive;
            prod.AddDate = prdt.AddDate;
            prod.BestSellers = prdt.BestSellers;
            prod.Highlights = prdt.Highlights;

            
            
            db.Entry(prod).State = System.Data.Entity.EntityState.Modified;

            db.SaveChanges();
            return RedirectToAction("ProductUpdateDelete");
        }
        public ActionResult ProductDelete(int id) 
        {
            Products prd = db.Products.FirstOrDefault(p => p.ProductID == id );
            db.Products.Remove(prd);
            db.SaveChanges();

            return RedirectToAction("ProductUpdateDelete");
        }
        public ActionResult CategoryUpdateDelete()
        {
            List<Categories> cat = db.Categories.OrderByDescending(p => p.Categoryıd).ToList();
            return View(cat);
        }
        public ActionResult CategoryUpdate(int id)
        {
            List<Categories> catlist = db.Categories.Where(c => c.ParentID==0).ToList();
            ViewData["catlist"] = catlist.Select(c => new SelectListItem
            { Text = c.CategoryName, Value = c.Categoryıd.ToString() }).ToList();

            Categories cat = db.Categories.FirstOrDefault(p => p.Categoryıd == id);

            return View(cat);
        }
        [HttpPost]
        public ActionResult CategoryUpdate(Categories cat)
        {
            vw_kategoriler ctl = db.vw_kategoriler.FirstOrDefault(p => p.Categoryıd == cat.Categoryıd);
            //kontrol et cat = ctl olmadığı için güncellemeiyor 

            cat.isActive = ctl.isActive;
            if (ctl.ParentID ==null & ctl.ParentID ==0)
            {
                cat.ParentID = ctl.ParentID;
                
            }
            else if (cat.ParentID ==null & ctl.ParentID>0)
            {
                cat.ParentID = 0;
            }
           
            
            db.Entry(ctl).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CategoryUpdateDelete");
        }
        public ActionResult CategoryDelete(int id)
        {
            List<Categories> catlist = db.Categories.Where(p => p.Categoryıd == id).ToList();
            foreach (var item in catlist)
            {
                db.Categories.Remove(item);
                db.SaveChanges();
            }

            Categories delc = db.Categories.FirstOrDefault(p => p.Categoryıd ==id);
            db.Categories.Remove(delc);
            db.SaveChanges();

            return RedirectToAction("ProductUpdateDelete");
        }
    }
}