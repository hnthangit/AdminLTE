using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class ProductPaginationResult : PaginationResult
    {
        public List<Product> Data;
        public Product DataProducts;
        public List<ProductAttributes> DataProductAttributes;
    }
}