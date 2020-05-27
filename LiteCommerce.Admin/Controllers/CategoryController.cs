using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        /// <summary>
        /// Trang hiển thị danh sách các Category
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 999999;
            var model = new Models.CategoryResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = CatalogBLL.Category_Count(searchValue),
                Data = CatalogBLL.Category_List(page, pageSize, searchValue),
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
                ViewBag.Title = "Add new Category";
                Category newCategory = new Category();
                newCategory.CategoryID = 0;
                return View(newCategory);
            }
            else
            {
                Category editCategory = CatalogBLL.GetCategory(Convert.ToInt32(id));
                if (editCategory == null)
                    return RedirectToAction("Index");
                ViewBag.Title = "Edit new Category";

                return View(editCategory);
            }
        }
    }
}