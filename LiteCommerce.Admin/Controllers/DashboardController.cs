using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    public class DashboardController : Controller
    {
        // GET: Dashboard

        public ActionResult Index()
        {
            return View();
        }
    }
}