using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IProductAttributeDAL
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(ProductAttributes data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(ProductAttributes data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Delete(int[] productAttributesIDs);

        /// <summary>
        ///
        /// </summary>
        /// <param name="productAttributeID"></param>
        /// <returns></returns>
        ProductAttributes Get(int productAttributeID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<ProductAttributes> List(int page, int pageSize, string searchValue);

        List<ProductAttributes> List(int productID);
    }
}