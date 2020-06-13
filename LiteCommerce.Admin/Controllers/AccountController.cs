using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
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
        /// <summary>
        /// Trang chủ mặc định
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            /*Employee data = AccountBLL.GetPass(User.Identity.Name);
            return View(data);*/
            WebUserData userData = User.GetUserData();
            Employee data = AccountBLL.GetPass(userData.UserID);
            return View(data);
        }

        /// <summary>
        /// Trang thay đổi mật khẩu
        /// </summary>
        /// <returns></returns>
        public ActionResult ChangePassword(string email, string currentpassword, string password, string confirmpassword)
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                Employee employee = AccountBLL.GetPass(email);
                if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(confirmpassword))
                {
                    ModelState.AddModelError("errorNull", "Mật khẩu không được để trống");
                    return View();
                }
                //if (currentpassword.Equals(employee.Password))
                if (String.Equals(employee.Password, EncodeMD5.GetMD5(currentpassword)))
                {
                    if (password.Equals(confirmpassword))
                    {
                        bool result = AccountBLL.Update(EncodeMD5.GetMD5(password), email);
                        if (result)
                        {
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else
                        {
                            return View();
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("errorCurrentpass", "Mật khẩu nhập lại không trùng khớp");
                        return View();
                    }
                }
                else
                {
                    ModelState.AddModelError("errorOlderpass", "Mật khẩu cũ không đúng");
                    return View();
                }
            }
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
        public ActionResult Login(string email = "", string password = "")
        {
            if (Request.HttpMethod == "GET")
            {
                return View();
            }
            else
            {
                //TODO: Kiểm tra thông tin đăng nhập qua CSDL
                var userAccount = AccountBLL.Authorize(email, EncodeMD5.GetMD5(password), UserAccountTypes.Employee);
                if (userAccount != null)
                {
                    WebUserData cookiesData = new Admin.WebUserData()
                    {
                        UserID = userAccount.UserID,
                        FullName = userAccount.FullName,
                        GroupName = userAccount.GroupName,
                        LoginTime = DateTime.Now,
                        SessionID = Session.SessionID,
                        ClientIP = Request.UserHostAddress,
                        Photo = userAccount.Photo,
                    };
                    FormsAuthentication.SetAuthCookie(cookiesData.ToCookieString(), false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Đăng nhập thất bại");
                    ViewBag.Email = email;
                    return View();
                }
                /*bool result = AccountBLL.Login(email, password);
                if (result)
                {
                    FormsAuthentication.SetAuthCookie(email, false);
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Đăng nhập thất bại");
                    ViewBag.Email = email;
                    return View();
                }*/
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

        /// <summary>
        /// Trang thay đổi thông tin cá nhân
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ChangeInfo(Employee data)
        {
            if (data != null)
            {
                Employee getEmployee = HumanResourceBLL.GetEmployee(data.EmployeeID);
                data.PhotoPath = getEmployee.PhotoPath;
                data.Password = getEmployee.Password;
                data.Notes = getEmployee.Notes;
                bool editUser = HumanResourceBLL.Employee_Update(data);
            }
            return RedirectToAction("Index");
        }
    }
}