using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [Authorize]
    public class ShipperController : Controller
    {
        // GET: Shipper
        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSizeWithoutPagination = 999999;
            var model = new Models.ShipperResult()
            {
                Page = page,
                PageSize = pageSizeWithoutPagination,
                RowCount = CatalogBLL.Shipper_Count(searchValue),
                Data = CatalogBLL.Shipper_List(page, pageSizeWithoutPagination, searchValue),
                searchValue = searchValue,
            };
            return View(model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Shipper";
                Shipper newShipper = new Shipper();
                newShipper.ShipperID = 0;
                return View(newShipper);
            }
            else
            {
                Shipper editShipper = CatalogBLL.GetShipper(Convert.ToInt32(id));
                if (editShipper == null)
                    return RedirectToAction("Index");
                ViewBag.Title = "Edit new Shipper";
                return View(editShipper);
            }
        }
    }
}