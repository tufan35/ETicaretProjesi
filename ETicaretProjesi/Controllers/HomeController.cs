using ETicaretProjesi.Models;
using ETicaretProjesi.MVVM;
using PagedList;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web.Mvc;

namespace ETicaretProjesi.Controllers
{
    public class HomeController : BaseController
    {
        ETicaretDBEntities db = new ETicaretDBEntities();
        AnasayfaModel ans = new AnasayfaModel();

        // GET: Home
        public ActionResult Index()
        {
            ans.SliderUrunler = Urunler.SliderUrunlerGetir();
            ans.EnYeniUrunler = Urunler.EnYeniUrunlerGetir("anasayfa","");
            ans.OzelUrunler = Urunler.OzelUrunlerGetir("anasayfa", "");
            ans.IndirimliUrunler = Urunler.IndirimliUrunlerGetir("anasayfa", "");
            ans.OneCikanUrunler = Urunler.OneCikanUrunlerGetir();
            ans.CokSatanlar = Urunler.CokSatanlarGetir();
            ans.SuperUrunler = Urunler.SuperUrunlerGetir();
            ans.GununUrunu = Urunler.GununUrununuGetir();
            return View(ans);
        }
        public ActionResult Detaylar(int id)
        {
            Urunler.HighlightsAdd(id);


            return View();
        }
        public ActionResult Sepet(int id)
        {
            Urunler.HighlightsAdd(id);


            return View();
        }
        public ActionResult Giris()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Giris(Users users)
        {
            users.password = Usr.md5Password(users.password);
            Users user = db.Users.FirstOrDefault(u => u.email == users.email && u.password == users.password);

            if (user != null)
            {
                Session["namesurname"] = user.namesurname;
                Session["UserID"] = user.UserID;
                if (user.isAdmin==true)
                {
                    Session["adminmi"] = user.isAdmin;
                }
                return RedirectToAction("Index", "Home");
            }

            return View();
        }
        public ActionResult UyeOl()
        {
          
            return View();
        }
        public ActionResult captcha()
        {

            Bitmap bmp = new Bitmap(60,40);
            Graphics graph  = Graphics.FromImage(bmp);
            graph.Clear(Color.Red);
            graph.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            Font font = new Font("Ariel" ,8,FontStyle.Bold);

            string randstr = "";
            int[] myar = new int[5];
            Random random  =  new Random();
            for (int i = 0; i < myar.Length; i++)
            {
                myar[i] = random.Next(0,9);
                randstr += myar[i].ToString();
            }
            Session["randstr"] = randstr;
            
            Usr usr = new Usr();
            usr.captcha = randstr;

            graph.DrawString(randstr,font,Brushes.White,3,3);
            Response.ContentType = "image/GIF";
            bmp.Save(Response.OutputStream,ImageFormat.Gif);
            font.Dispose();
            graph.Dispose();
            bmp.Dispose();

            return View();

        }
        [HttpPost]
        public ActionResult UyeOl(Users users)
        {


            string txtcaptcha = Request.Form["txtcaptcha"];

            if (txtcaptcha == Session["randstr"].ToString())
            {
                bool sonuc = Usr.uyekaydet(users);
                if (sonuc)
                {
                    Session["Mesaj"] = "Uyeliğiniz başarı ile gerçekleşti.";
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    Session["Mesaj"] = "Bu email vardır üue kaydı gerçekleşmedi";
                    return View();
                }



               
            }
            else
            {
                Session["Mesaj"] = "Uyeliğiniz gerçekleşmedi";
                return View();
            }


            
        }
        public ActionResult EnYeniUrunler()
        {
            ans.EnYeniUrunler = Urunler.EnYeniUrunlerGetir("enyeniurunler","");
           
            return View(ans);
        }
        public ActionResult _partial_EnYeniUrunler(string sonrakisayfa)
        {
            ans.EnYeniUrunler = Urunler.EnYeniUrunlerGetir("enyeniurunler", sonrakisayfa);

            return View(ans);
            
        }
        public ActionResult OzelUrunler()
        {
            ans.OzelUrunler = Urunler.OzelUrunlerGetir("ozelurunler", "");

            return View(ans);
        }
        public ActionResult _partial_OzelUrunler(string sayfano)
        {
            ans.OzelUrunler = Urunler.OzelUrunlerGetir("_partial_OzelUrunler", sayfano);
            return View(ans);
        }
        public ActionResult IndirimliUrunler()
        {
            ans.IndirimliUrunler = Urunler.IndirimliUrunlerGetir("indirimliurunler", "");
            return View(ans);  
        }
        public ActionResult _partial_IndirimliUrunler(string sayfano)
        {
            ans.IndirimliUrunler = Urunler.IndirimliUrunlerGetir("indirimliurunler_partial", sayfano);
            return View(ans);
        }
        public ActionResult OneCikanUrunler(int? page)
        {
            var list = db.vw_aktif_urunler.OrderBy(p => p.Highlights).ToList();
            var pagenumber = page ?? 1;
            var sayfalidata = list.ToPagedList(pagenumber,8);
            ViewBag.OneCikanUrunler = sayfalidata;
            return View();
        }
        public ActionResult CokSatanUrunler(int? page)
        {
            var list = db.vw_aktif_urunler.OrderBy(p => p.BestSellers).ToList();
            var pagenumber = page ?? 1;
            var sayfalidata = list.ToPagedList(pagenumber, 8);
            ViewBag.CokSatanUrunler = sayfalidata;
            return View();
        }
        public ActionResult Markalar(int id, int? page) 
        {
            string adi = db.Suppliers.FirstOrDefault(c => c.SupplierID == id ).BrandName;
            ViewBag.markaAdi = adi;

            var list = Urunler.MarkalıUrunGetir(id);
            var pagenumber = page ?? 1;
            var sayfalidata = list.ToPagedList(pagenumber, 4);
            ViewBag.Markalarimiz = sayfalidata;
            return View();

        }

        public ActionResult Kategoriler(int id, int? page)
        {
            string adi = db.Categories.FirstOrDefault(c => c.Categoryıd == id).CategoryName;
            ViewBag.kategoriAdi = adi;

            var list = Urunler.KategoriliUrunGetir(id);
            var pagenumber = page ?? 1;
            var sayfalidata = list.ToPagedList(pagenumber, 4);
            ViewBag.Kategorilerimiz = sayfalidata;
            return View();

        }


    }
}