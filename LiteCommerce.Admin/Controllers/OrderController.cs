using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [AuthorizeRedirect(Roles = WebUserRoles.SALEMAN)]
    public class OrderController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        [Authorize]
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}