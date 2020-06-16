using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Customer
    /// </summary>
    public interface ICustomerDAL
    {
        /// <summary>
        /// Thêm một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Customer data);

        /// <summary>
        /// Cập nhật một khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);

        /// <summary>
        /// Xóa một hoặc nhiều khách hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int[] customerIDs);

        /// <summary>
        /// Lấy thông tin của một khách hàng theo mã ID
        /// </summary>
        /// <param name="customerID">Mã khách hàng</param>
        /// <returns></returns>
        Customer Get(string customerID);

        /// <summary>
        /// Lấy danh sách các khách hàng theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Customer> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Lấy thông tin của một khách hàng
        /// </summary>
        /// <returns></returns>
        List<Customer> List();

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}