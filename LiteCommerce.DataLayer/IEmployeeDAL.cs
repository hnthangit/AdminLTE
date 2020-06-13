using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IEmployeeDAL
    {
        int Add(Employee data);

        bool Update(Employee data);

        bool Update(string email, string password);

        bool Delete(int[] employeeIDs);

        Employee Get(int employeeID);

        List<Employee> List(int page, int pageSize, string searchValue);

        int Count(string searchValue);

        bool Login(string email, string password);

        Employee Get(string email);
    }
}