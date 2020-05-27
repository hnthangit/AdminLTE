using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class CustomerPaginationResult : PaginationResult
    {
        public List<Customer> Data;
    }
}