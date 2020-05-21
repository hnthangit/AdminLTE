using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class AccountController : Controller
    {
        // GET: Account
        /// <summary>
        /// Trang chủ mặc định
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Trang thay đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword()
        {
            return View();
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns>~/Login -- chuyển về trang đăng nhập</returns>
        public ActionResult SignOut()
        {
            Session.Abandon();
            Session.Clear();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
        /// <summary>
        /// Trang đăng nhập
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Login(string email ="", string password ="")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                //TODO: 
                if(email =="admin@abc.com" & password == "123")
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("", "Đăng nhập thất bại");
                    ViewBag.Email = email;
                    return View();
                }
            }
        }

        /// <summary>
        /// Trang lấy lại mật khẩu
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPass()
        {
            return View();
        }
    }
}