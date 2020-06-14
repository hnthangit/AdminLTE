using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Category
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        /// Thêm một thể loại vào database
        /// </summary>
        /// <param name="data">Dữ liệu của một Category</param>
        /// <returns>ID của thể loại vừa thêm</returns>
        int Add(Category data);

        /// <summary>
        /// Cập nhật một thể loại đã có trong database
        /// </summary>
        /// <param name="data">Dữ liệu của một Category</param>
        /// <returns>True nếu update được và ngược lại trả về False </returns>
        bool Update(Category data);

        /// <summary>
        /// Xóa một hoặc nhiều thể loại
        /// </summary>
        /// <param name="categoryIDs">Mã thể loại</param>
        /// <returns>True nếu xóa được và ngược lại trả về False</returns>
        bool Delete(int[] categoryIDs);

        /// <summary>
        /// Lấy thông tin của một thể loại
        /// </summary>
        /// <param name="catogoryID">Mã thể loại</param>
        /// <returns>Thông tin của một thể loại</returns>
        Category Get(int catogoryID);

        /// <summary>
        /// Danh sách thể loại trong database
        /// </summary>
        /// <param name="page">Số trang</param>
        /// <param name="pageSize">Kích thước của một trang</param>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns>Danh sách các thể loại theo phân trang</returns>
        List<Category> List(int page, int pageSize, string searchValue);

        /// <summary>
        /// Đếm số lượng truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue">Giá trị cần tìm kiếm</param>
        /// <returns>Số lượng truy vấn tìm kiếm được</returns>
        int Count(string searchValue);

        /// <summary>
        /// Danh sách toàn bộ thể loại
        /// </summary>
        /// <returns>Danh sách toàn bộ thể loại</returns>
        List<Category> List();
    }
}