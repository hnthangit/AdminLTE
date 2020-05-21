using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Hàm thêm một supplier
        /// </summary>
        /// <param name="data">Giá trị của supplier được thêm vào</param>
        /// <returns>ID của supplier được bổ sung (<=0 nếu lỗi)</returns>
        int Add(Supplier data);
        /// <summary>
        /// Hàm thay đổi một supplier
        /// </summary>
        /// <param name="data">Giá trị của supplier cần thay đổi</param>
        /// <returns>Trả về giá trị true nếu thành công và false khi thất bại </returns>
        bool Update(Supplier data);
        /// <summary>
        /// Hàm xóa một supplier
        /// </summary>
        /// <param name="supplierIDs">Giá trị của supplier được xóa</param>
        /// <returns>Trả về giá trị true nếu thành công và false khi thất bại</returns>
        bool Delete(int[] supplierIDs);
        /// <summary>
        /// Hàm lấy giá trị ID của supplier
        /// </summary>
        /// <param name="supplierID">ID cần truyền vào</param>
        /// <returns>Một Supplier theo ID được truyền vào</returns>
        Supplier Get(int supplierID);
        /// <summary>
        /// Hàm hiển thị dữ liệu khi tìm kiếm
        /// </summary>
        /// <param name="page">Trang thứ mấy</param>
        /// <param name="pageSize">Tổng số trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns></returns>
        List<Supplier> List(int page, int pageSize, string searchValue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}