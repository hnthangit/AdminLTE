using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    ///
    /// </summary>
    public interface ICategoryDAL
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Category data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Category data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="categoryIDs"></param>
        /// <returns></returns>
        bool Delete(int[] categoryIDs);

        /// <summary>
        ///
        /// </summary>
        /// <param name="catogoryID"></param>
        /// <returns></returns>
        Category Get(int catogoryID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Category> List(int page, int pageSize, string searchValue);

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        List<Category> List();
    }
}