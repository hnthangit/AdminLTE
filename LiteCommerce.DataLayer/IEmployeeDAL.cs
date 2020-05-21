using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    internal interface IEmployeeDAL
    {
        int Add(Employee data);

        bool Update(Employee data);

        bool Delete(Employee data);

        Employee Get(int employeeID);

        List<Employee> List(int page, int pageSize, string searchValue);
    }
}