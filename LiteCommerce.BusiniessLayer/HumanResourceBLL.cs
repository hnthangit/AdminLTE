using LiteCommerce.DataLayer;
using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.BusiniessLayer
{
    /// <summary>
    /// Nguồn nhân lực
    /// </summary>
    public static class HumanResourceBLL
    {
        private static IEmployeeDAL EmployeeDB { get; set; }

        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayer.SqlServer.EmployeeDAL(connectionString);
        }

        #region Employee

        /// <summary>
        /// Danh sách các nhân viên theo phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> Employee_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return EmployeeDB.List(page, pageSize, searchValue);
        }

        /// <summary>
        /// Đếm số lượng kết quả truy vấn tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static int Employee_Count(string searchValue)
        {
            return EmployeeDB.Count(searchValue);
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo ID
        /// </summary>
        /// <param name="employeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }

        /// <summary>
        /// Thêm thông tin một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int Employee_Add(Employee data)
        {
            return EmployeeDB.Add(data);
        }

        /// <summary>
        /// Cập nhật thông tin của một nhân viên
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool Employee_Update(Employee data)
        {
            return EmployeeDB.Update(data);
        }

        /// <summary>
        /// Xóa một hoặc nhiều nhân viên theo ID
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        public static bool Employee_Delete(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }

        #endregion Employee
    }
}