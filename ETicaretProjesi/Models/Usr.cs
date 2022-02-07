using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace ETicaretProjesi.Models
{
    public class Usr
    {
        public string captcha  { get; set; }


        public static bool uyekaydet(Users users) 
        {
            using(ETicaretDBEntities db = new ETicaretDBEntities())
            {
                Users us = db.Users.FirstOrDefault(u => u.email ==users.email);
                if(us != null)
                {
                    return false;

                }
                else
                {
                    users.isActive = true;
                    users.isAdmin = false;
                    users.password = md5Password(users.password);   
                    db.Users.Add(users);
                    db.SaveChanges();
                    return true;
                }
            }
        
        }

        public static string md5Password(string text) 
        {
           MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] btr = Encoding.UTF8.GetBytes(text);
            btr =  md5.ComputeHash(btr);       

            StringBuilder  sb  = new StringBuilder();
            foreach (var item in btr)
            {
                sb.Append(item.ToString("x2").ToLower());

            }
            return sb.ToString();
        }


    }
}