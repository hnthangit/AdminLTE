using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class CategoryResult : PaginationResult
    {
        public List<Category> Data;
    }
}