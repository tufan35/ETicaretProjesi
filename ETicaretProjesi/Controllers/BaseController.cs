using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ETicaretProjesi.Models;
namespace ETicaretProjesi.Controllers
{
    public class BaseController : Controller
    {
        ETicaretDBEntities db = new ETicaretDBEntities();
        public BaseController()
        {
            ViewBag.CategoryList = db.Categories.Where(c => c.isActive== true ).OrderBy(c => c.CategoryName).ToList();
            ViewBag.SupplierList = db.Suppliers.Where(s => s.IsActive == true).OrderBy(s => s.BrandName).ToList();

        }
    }
}