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
    public class SupplierController : Controller
    {
        /// <summary>
        /// Trang hiển thị: danh sách các supplier, các "liên kết" đến các chứ năng liên quan
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = AppSetting.DefaultPageSize,
                RowCount = CatalogBLL.Supplier_Count(searchValue),
                Data = CatalogBLL.Supplier_List(page, AppSetting.DefaultPageSize, searchValue),
                searchValue = searchValue,
            };

            return View(model);
        }

        /// <summary>
        /// Add or edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Input(string id = "")
        {            
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Supplier";
                Supplier newSupplier = new Supplier();
                newSupplier.SupplierID = 0;
                return View(newSupplier);
            }
            else
            {
                Supplier editSupplier = CatalogBLL.GetSupplier(Convert.ToInt32(id));
                if (editSupplier == null)
                    return RedirectToAction("Index");
                ViewBag.Title = "Edit new Supplier";
                return View(editSupplier);

            }
        }
    }
}