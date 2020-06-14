using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface ShipperDAL
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        /// Thêm một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns>Mã người giao hàng</returns>
        int Add(Shipper data);

        /// <summary>
        /// Cập nhật một người giao hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        /// Xóa một hoặc nhiều người giao hàng theo mã người giao hàng
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
        bool Delete(int[] shipperIDs);

        /// <summary>
        /// Lấy thông tin theo người giao hàng theo ID
        /// </summary>
        /// <param name="shipperID">Mã người giao hàng</param>
        /// <returns>Thông tin của môt người giao hàng</returns>
        Shipper Get(int shipperID);

        /// <summary>
        /// Danh sách người giao hàng không phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số lượng truy vấn tìm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns>Số lượng kết quả tìm được</returns>
        int Count(string searchValue);
    }
}