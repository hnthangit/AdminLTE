using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
    public class CustomerController : Controller
    {
        // GET: Customer
        /// <summary>
        /// Trang chủ
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = AppSetting.DefaultPageSize,
                RowCount = CatalogBLL.Customer_Count(searchValue),
                Data = CatalogBLL.Customer_List(page, AppSetting.DefaultPageSize, searchValue),
                searchValue = searchValue,
            };
            return View(model);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add Customer";
                Customer newCustomer = new Customer();
                newCustomer.CustomerID = null;
                return View(newCustomer);
            }
            else
            {
                Customer editCustomer = CatalogBLL.GetCustomer(id);
                if (editCustomer == null)
                    return RedirectToAction("Index");
                ViewBag.Title = "Edit Customer";
                return View(editCustomer);
            }
        }

        [HttpPost]
        public ActionResult Input(Customer data)
        {
            try
            {
                #region Allow Null for Columns

                if (string.IsNullOrEmpty(data.CompanyName))
                {
                    data.CompanyName = "";
                }
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
                if (string.IsNullOrEmpty(data.Country))
                {
                    data.Country = "";
                }
                if (string.IsNullOrEmpty(data.Phone))
                {
                    data.Phone = "";
                }
                if (string.IsNullOrEmpty(data.Fax))
                {
                    data.Fax = "";
                }

                #endregion Allow Null for Columns

                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                Customer customer = CatalogBLL.GetCustomer(data.CustomerID);
                if (customer == null)
                {
                    int customerId = CatalogBLL.Customer_Add(data);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Customer_Update(data);
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(data);
            }
        }

        public ActionResult Delete(string method = "", int[] customerIDs = null)
        {
            if (customerIDs != null)
            {
                CatalogBLL.Customer_Delete(customerIDs);
            }
            return RedirectToAction("Index");
        }
    }
}