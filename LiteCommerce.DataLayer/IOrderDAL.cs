using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Order
    /// </summary>
    public interface IOrderDAL
    {
        /// <summary>
        /// Thêm một đơn đặt hàng
        /// </summary>
        /// <param name="data">Dữ liệu của một đơn hàng</param>
        /// <returns>Mã đơn hàng sau khi thêm</returns>
        int Add(Order data);

        /// <summary>
        /// Cập nhật một đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Order data);

        /// <summary>
        /// Xóa một hay nhiều đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderIDs"></param>
        /// <returns></returns>
        bool Delete(int[] orderIDs);

        /// <summary>
        /// Lấy thông tin của một đơn hàng theo mã đơn hàng
        /// </summary>
        /// <param name="orderID">Mã đơn hàng</param>
        /// <returns></returns>
        Order Get(int orderID);

        /// <summary>
        /// Danh sách các đơn hàng theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <param name="customerId">Mã khách hàng</param>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <param name="shipperId">Mã người giao hàng</param>
        /// <returns></returns>
        List<Order> List(int page, int pageSize, string searchValue, int customerId, int employeeId, int shipperId);

        /// <summary>
        /// Đếm số truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <param name="customerId">Mã khách hàng</param>
        /// <param name="employeeId">Mã nhân viên</param>
        /// <param name="shipperId">Mã người giao hàng</param>
        /// <returns>Số kết quả tìm được</returns>
        int Count(string searchValue, int customerId, int employeeId, int shipperId);
    }
}