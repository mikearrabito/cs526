﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ImageSharingWithUpload.Controllers
{
    public class AccountController : Controller
    {
        protected void CheckAda()
        {
            var cookie = Request.Cookies["ADA"];
            if (cookie != null && "true".Equals(cookie))
            {
                ViewBag.isADA = true;
            }
            else
            {
                ViewBag.isADA = false;
            }
        }

        [HttpGet]
        public ActionResult Register()
        {
            CheckAda();
            return View();
        }

        [HttpPost]
        public ActionResult Register(String Userid, String ADA)
        {
            CheckAda();

            var options = new CookieOptions()
            {
                IsEssential = true,
                Expires = DateTime.Now.AddMonths(3),
                SameSite = SameSiteMode.Lax
            };

            Console.WriteLine(Userid);

            bool isAda = ADA != null && ADA == "on";

            if (isAda)
            {
                Console.WriteLine("ADA on");
                ADA = "true";
            }
            else
            {
                Console.WriteLine("ADA off");
                ADA = "false";
            }

            Response.Cookies.Append("ADA", ADA, options);
            Response.Cookies.Append("userid", Userid, options);

            ViewBag.isADA = isAda;
            ViewBag.Userid = Userid;

            return View("RegisterSuccess");
        }

    }
}