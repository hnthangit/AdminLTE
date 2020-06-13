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
        /// Trang Category/Input : thêm mới một Category
        /// Nếu có tham số ID thì chuyển về trang chỉnh sửa
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
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

        /// <summary>
        /// Trang thêm thông tin của một Category
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Category data)
        {
            try
            {
                #region Allow Null for Columns

                if (string.IsNullOrEmpty(data.CategoryName))
                {
                    data.CategoryName = "";
                }

                #endregion Allow Null for Columns

                if (string.IsNullOrEmpty(data.CategoryName))
                    ModelState.AddModelError("ErrorCategoryName", "Category Name is required");
                if (!ModelState.IsValid)
                {
                    return View(data);
                }
                if (data.CategoryID == 0)
                {
                    int categoryId = CatalogBLL.Category_Add(data);
                    return RedirectToAction("Index");
                }
                else
                {
                    bool updateResult = CatalogBLL.Category_Update(data);
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
        /// Action xóa một hoặc nhiều Category
        /// </summary>
        /// <param name="method"></param>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        public ActionResult Delete(string method = "", int[] categoryIDs = null)
        {
            if (categoryIDs != null)
            {
                CatalogBLL.Category_Delete(categoryIDs);
            }
            return RedirectToAction("Index");
        }
    }
}