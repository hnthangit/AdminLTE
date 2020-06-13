using LiteCommerce.DataLayer;
using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.BusiniessLayer
{
    public static class HumanResourceBLL
    {
        private static IEmployeeDAL EmployeeDB { get; set; }

        public static void Initialize(string connectionString)
        {
            EmployeeDB = new DataLayer.SqlServer.EmployeeDAL(connectionString);
        }

        #region Employee

        public static List<Employee> Employee_List(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize <= 0)
                pageSize = 30;
            return EmployeeDB.List(page, pageSize, searchValue);
        }

        public static int Employee_Count(string searchValue)
        {
            return EmployeeDB.Count(searchValue);
        }

        public static Employee GetEmployee(int employeeID)
        {
            return EmployeeDB.Get(employeeID);
        }

        public static int Employee_Add(Employee data)
        {
            return EmployeeDB.Add(data);
        }

        public static bool Employee_Update(Employee data)
        {
            return EmployeeDB.Update(data);
        }

        public static bool Employee_Delete(int[] employeeIDs)
        {
            return EmployeeDB.Delete(employeeIDs);
        }      

        #endregion Employee
    }
}