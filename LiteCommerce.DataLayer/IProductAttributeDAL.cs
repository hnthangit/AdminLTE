using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IProductAttributeDAL
    {
        int Add(ProductAttributes data);

        bool Update(ProductAttributes data);

        bool Delete(ProductAttributes data);

        ProductAttributes Get(int productAttributeID);

        List<ProductAttributes> List(int page, int pageSize, string searchValue);
    }
}