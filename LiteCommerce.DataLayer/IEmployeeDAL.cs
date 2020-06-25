using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Employee
    /// </summary>
    public interface IEmployeeDAL
    {
        /// <summary>
        /// Thêm một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Employee data);

        /// <summary>
        /// Cập nhật một nhân viên
        /// </summary>
        /// <param name="data">Thông tin một nhân viên</param>
        /// <returns></returns>
        bool Update(Employee data);

        /// <summary>
        /// Cập nhật mật khẩu theo email
        /// </summary>
        /// <param name="email">Email đăng nhập</param>
        /// <param name="password">Mật khẩu</param>
        /// <returns></returns>
        bool Update(string email, string password);

        /// <summary>
        /// Xóa một hoặc nhiều nhân viên
        /// </summary>
        /// <param name="employeeIDs">Mã nhân viên</param>
        /// <returns></returns>
        bool Delete(int[] employeeIDs);

        /// <summary>
        /// Lấy thông tin một nhân viên
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        Employee Get(int employeeID);

        /// <summary>
        /// Kiểm tra email khi thêm nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="email">Email nhập vào</param>
        /// <returns>true nếu đã tồn tại và ngược lại thì false</returns>
        bool IsEmailExist(string email,int employeeId);

        /// <summary>
        /// Danh sách các nhân viên phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Employee> List(int page, int pageSize, string searchValue,string country);

        /// <summary>
        /// Danh sách tất cả các nhan viên
        /// </summary>
        /// <returns></returns>
        List<Employee> List();

        /// <summary>
        /// Đếm số lượng truy vấn tìm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue,string country);

        /// <summary>
        /// Kiểm tra đăng nhập
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool Login(string email, string password);

        /// <summary>
        /// Lấy thông tin của một nhân viên theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Employee Get(string email);
    }
}