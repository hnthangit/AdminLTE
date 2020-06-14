using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IProductDAL
    {
        /// <summary>
        /// Thêm một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Product data);

        /// <summary>
        /// Cập nhật một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Product data);

        /// <summary>
        /// Xóa một hoặc nhiều sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int[] productIDs);

        /// <summary>
        /// Lấy thông tin một sản phẩm theo ID
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        Product Get(int productID);

        /// <summary>
        /// Danh sách thông tin sản phẩm theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Product> List(int page, int pageSize, string searchValue, int categoryName, int companyName);

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns>Số lượng kết quả tìm kiếm được</returns>
        int Count(string searchValue, int categoryName, int companyName);
    }
}