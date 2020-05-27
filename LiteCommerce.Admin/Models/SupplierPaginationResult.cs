using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class SupplierPaginationResult : PaginationResult
    {
        public List<Supplier> Data;
    }
}