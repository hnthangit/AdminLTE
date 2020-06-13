using System;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    public class _TestController : Controller
    {
        //[Authorize(Roles = WebUserRoles.SALEMAN)]
        [AuthorizeRedirect(Roles = "Saleman")]
        public ActionResult CheckAuth()
        {
            if (User.Identity.IsAuthenticated)
            {
                return Json(User.GetUserData(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "_Test");
            }
            /*string a = Convert.ToString(User.Identity.IsAuthenticated);
            return Content(a);*/
        }

        [AuthorizeRedirect]
        public ActionResult Index()
        {
            return View();
        }

    }
}