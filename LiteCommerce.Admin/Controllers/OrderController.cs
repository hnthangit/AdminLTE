using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
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
        public ActionResult Index(int page = 1, string searchValue = "", string customerId = "", int employeeId = 0, int shipperId = 0)
        {
            int _pageSize = 50;
            var model = new Models.OrderPaginationResult()
            {
                Page = page,
                PageSize = _pageSize,
                RowCount = CatalogBLL.Order_Count(searchValue, customerId, employeeId, shipperId),
                Data = CatalogBLL.Order_List(page, _pageSize, searchValue, customerId, employeeId, shipperId),
                searchValue = searchValue,
                customerId = customerId,
                employeeId = employeeId,
                shipperId = shipperId,
            };
            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Detail(string id = "")
        {
            if (!String.IsNullOrEmpty(id))
            {
                Order data = CatalogBLL.GetOrder(Convert.ToInt32(id));
                if (data == null)
                {
                    return RedirectToAction("Index", "Order");
                }
                else
                {
                    ViewData["OrderDetail"] = CatalogBLL.OrderDetails_List(data.OrderID);
                    return View(data);
                }
            }
            else
            {
                return RedirectToAction("Index", "Order");
            }
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
                ViewBag.Title = "Add new Order";
                Order newOrder = new Order();
                newOrder.OrderID = 0;
                return View(newOrder);
            }
            else
            {
                Order editOrder = CatalogBLL.GetOrder(Convert.ToInt32(id));
                if (editOrder == null)
                    return RedirectToAction("Index");
                ViewData["OrderDetail"] = CatalogBLL.OrderDetails_List(editOrder.OrderID);

                ViewBag.Title = "Edit Order";
                return View(editOrder);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Order data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                if (data.OrderID == 0)
                {
                    int orderId = CatalogBLL.Order_Add(data);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Order_Update(data);
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
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        public ActionResult Delete(int[] orderIDs)
        {
            if(orderIDs != null)
            {
                var deleteOrder = CatalogBLL.Order_Delete(orderIDs);
            }
            return RedirectToAction("Index");
        }

    }
}