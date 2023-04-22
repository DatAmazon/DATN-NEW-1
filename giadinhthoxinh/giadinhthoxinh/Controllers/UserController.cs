using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using giadinhthoxinh.Models;

namespace giadinhthoxinh.Controllers
{
    public class UserController : Controller
    {
        giadinhthoxinhEntities db = new giadinhthoxinhEntities();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        //Login
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string sEmail, string sPass)
        {
            if(sEmail == "admin@gmail.com" && sPass == "123456")
            {
                Session["User"] = "admin";

                return RedirectToAction("products", "admin");
            }
            else if (ModelState.IsValid)
            {
                var f_password = GetMD5(sPass);
                var data = db.tblUsers.Where(s => s.sEmail.Equals(sEmail) && s.sPass.Equals(f_password)).ToList();
                if (data.Count() > 0)
                {
                    //add session
                    Session["idUser"] = data.FirstOrDefault().PK_iAccountID;
                    Session["userName"] = data.FirstOrDefault().sUserName;
                    Session["Email"] = data.FirstOrDefault().sEmail;
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.error = "Login failed";
                    return RedirectToAction("login");
                }
            }

            return View();
        }

        //Register    
        public ActionResult Register()
        {
            ViewBag.FK_iPermissionID = new SelectList(db.tblPermissions, "PK_iPermissionID", "sPermissionName");
            return View();
        }

        //public ActionResult Register1()
        //{
        //    return View();
        //}

        //POST: Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(tblUser _user)
        {
            if (ModelState.IsValid)
            {
                var check = db.tblUsers.FirstOrDefault(s => s.sEmail == _user.sEmail);
                if (check == null)
                {
                    _user.sPass = GetMD5(_user.sPass);
                    db.Configuration.ValidateOnSaveEnabled = false;
                    db.tblUsers.Add(_user);
                    db.SaveChanges();
                    return RedirectToAction("index", "home");
                }
                else
                {
                    ViewBag.error = "Email này đã tồn tại";
                    return View();
                }


            }
            //ViewBag.FK_iPermissionID = new SelectList(db.tblPermissions, "PK_iPermissionID", "sPermissionName", tblUser.FK_iPermissionID);
            return View();
        }

        //create a string MD5
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }

        public ActionResult Permission()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("index", "home");

        }

    }
}