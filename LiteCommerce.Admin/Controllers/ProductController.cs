using LiteCommerce.BusiniessLayer;
using LiteCommerce.DomainModels;
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [AuthorizeRedirect(Roles = WebUserRoles.ACCOUNTANT)]
    public class ProductController : Controller
    {
        // GET: Product
        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        [Authorize]
        public ActionResult Index(int page = 1, string searchValue = "", int categoryName = 0, int companyName = 0)
        {
            var model = new Models.ProductPaginationResult()
            {
                Page = page,
                PageSize = AppSetting.DefaultPageSize,
                RowCount = CatalogBLL.Product_Count(searchValue, categoryName, companyName),
                Data = CatalogBLL.Product_List(page, AppSetting.DefaultPageSize, searchValue, categoryName, companyName),
                searchValue = searchValue,
                categoryName = categoryName,
                companyName = companyName,
            };

            return View(model);
        }

        public ActionResult Detail(string id = "")
        {
            if (!String.IsNullOrEmpty(id))
            {
                Product model = CatalogBLL.GetProduct(Convert.ToInt32(id));
                if (model == null)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ViewData["Attribute"] = CatalogBLL.ProductAttribute_List(model.ProductID);
                    return View(model);
                }
            }
            else
            {
                return RedirectToAction("Index", "Product");
            }
        }

        /// <summary>
        /// Thêm hoặc sửa một sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Add new Product";
                Product newProduct = new Product();
                newProduct.ProductID = 0;
                return View(newProduct);
            }
            else
            {
                Product editProduct = CatalogBLL.GetProduct(Convert.ToInt32(id));
                if (editProduct == null)
                    return RedirectToAction("Index");
                ViewData["ProducAttribute"] = CatalogBLL.ProductAttribute_List(editProduct.ProductID);
                /*
                                if (model == null)
                                    return RedirectToAction("Index");*/
                ViewBag.Title = "Edit Product";
                return View(editProduct);
            }
        }

        /// <summary>
        /// Xử lý việc thêm hoặc sửa một sản phẩm
        /// </summary>
        /// <param name="data">Dữ liệu của một sản phẩm</param>
        /// <param name="file">File ảnh</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Product data, HttpPostedFileBase file = null)
        {
            if (data.Descriptions == null)
            {
                data.Descriptions = "";
            }
            if (file != null)
            {
                string get = DateTime.Now.ToString("ddMMyyyhhmmss");
                string fileExtension = Path.GetExtension(file.FileName);
                string fileName = get + fileExtension;
                string path = Path.Combine(Server.MapPath("~/Images"), fileName);
                data.PhotoPath = fileName;
                file.SaveAs(path);
            }
            if (data.ProductID == 0)
            {
                if (file == null)
                {
                    TempData["emptyFile"] = "Vui lòng chọn file";
                    return RedirectToAction("Input");
                }
                else
                {
                    int productID = CatalogBLL.Product_Add(data);
                    //return Content( productID.ToString());
                    TempData["productID"] = productID;
                    return RedirectToAction("AddProductAttribute");
                }
            }
            else
            {
                var getProduct = CatalogBLL.GetProduct(data.ProductID);
                if (file == null)
                {
                    data.PhotoPath = getProduct.PhotoPath;
                }
                bool updateProduct = CatalogBLL.Product_Update(data);
                return RedirectToAction("Index");
            }
        }

        /// <summary>
        /// Xóa một hay nhiều sản phẩm
        /// </summary>
        /// <param name="ProductIDs"></param>
        /// <returns></returns>
        public ActionResult Delete(int[] ProductIDs)
        {
            if (ProductIDs != null)
            {
                var deleteProduct = CatalogBLL.Product_Delete(ProductIDs);
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Xử lý thêm một thuộc tính sản phẩm
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult AddProductAttribute(string id = "")
        {
            return View();
        }

        /// <summary>
        /// Thêm hoặc sửa một thuộc tính của sản phẩm
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult InputProductAttribute(ProductAttributes model)
        {
            if (model.AttributeID == 0)
            {
                var addAttr = CatalogBLL.ProductAttribute_Add(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });
            }
            else
            {
                var editAttr = CatalogBLL.ProductAttributes_Update(model);
                return RedirectToAction("Input", "Product", new { id = model.ProductID });
            }
        }

        /// <summary>
        /// Xóa một hoặc nhiều thuộc tính của sản phẩm
        /// </summary>
        /// <param name="attributesIDs"></param>
        /// <param name="productID"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DeleteProductAttribute(int[] attributesIDs, string productID)
        {
            if (attributesIDs != null)
            {
                var deleteAttr = CatalogBLL.ProductAttributes_Delete(attributesIDs);
            }
            return RedirectToAction("Input", "Product", new { @id = Convert.ToInt32(productID) });
        }

        /// <summary>
        /// Xóa một hoặc nhiều sản phẩm
        /// </summary>
        /// <param name="method"></param>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        public ActionResult Delete(string method = "", int[] productIDs = null)
        {
            if (productIDs != null)
            {
                CatalogBLL.Product_Delete(productIDs);
            }
            return RedirectToAction("Index");
        }
    }
}