using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class EmployeePaginationResult : PaginationResult
    {
        public List<Employee> Data;
    }
}