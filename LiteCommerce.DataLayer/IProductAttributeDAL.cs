using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IProductAttributeDAL
    {
        /// <summary>
        /// Thêm thuộc tính của một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(ProductAttributes data);

        /// <summary>
        /// Cập nhật thuộc tính của một sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(ProductAttributes data);

        /// <summary>
        /// Xóa một hoặc nhiều thuộc tính của sản phẩm
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int[] productAttributesIDs);

        /// <summary>
        /// Lấy thông tin của các thuộc tính của một sản phẩm
        /// </summary>
        /// <param name="productAttributeID"></param>
        /// <returns></returns>
        ProductAttributes Get(int productAttributeID);

        /// <summary>
        /// Lấy danh sách thuộc tính của một sản phẩm có phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<ProductAttributes> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Danh sách thuộc tính của một sản phẩm theo mã sản phẩm
        /// </summary>
        /// <param name="productID"></param>
        /// <returns></returns>
        List<ProductAttributes> List(int productID);
    }
}