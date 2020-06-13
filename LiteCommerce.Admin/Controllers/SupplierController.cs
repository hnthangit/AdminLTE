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
    [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
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
        [HttpGet]
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost] // đây là 1 attribute
        public ActionResult Input(Supplier data)
        {
            try
            {
                //TODO: xu li validate dữ liệu đầu vào
                /// @Html.ValidationMessage("") : hiển thị lỗi theo key
                /// @Html.ValidationSummary() : hiển thị danh sách các lỗi
                
                if (string.IsNullOrEmpty(data.CompanyName))
                    ModelState.AddModelError("ErrorCompanyName", "Comany name is required");

                #region Allow Null for Columns

                if (string.IsNullOrEmpty(data.ContactName))
                {
                    data.ContactName = "";
                }
                if (string.IsNullOrEmpty(data.ContactTitle))
                {
                    data.ContactTitle = "";
                }
                if (string.IsNullOrEmpty(data.Address))
                {
                    data.Address = "";
                }
                if (string.IsNullOrEmpty(data.City))
                {
                    data.City = "";
                }
                if (string.IsNullOrEmpty(data.Phone))
                {
                    data.Phone = "";
                }
                if (string.IsNullOrEmpty(data.HomePage))
                {
                    data.HomePage = "";
                }
                if (string.IsNullOrEmpty(data.Fax))
                {
                    data.Fax = "";
                }
                if (string.IsNullOrEmpty(data.Country))
                {
                    data.Country = "";
                }

                #endregion Allow Null for Columns

                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                //TODO Xử lý để đưa dữ liệu theo database
                if (data.SupplierID == 0)
                {
                    int supplierId = CatalogBLL.Supplier_Add(data);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Supplier_Update(data);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(data);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="method"></param>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        public ActionResult Delete(string method = "", int[] supplierIDs = null)
        {
            if (supplierIDs != null)
            {
                CatalogBLL.Supplier_Delete(supplierIDs);
            }
            return RedirectToAction("Index");
        }
    }
}