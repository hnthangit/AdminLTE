using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Country
    /// </summary>
    public interface ICountryDAL
    {
        /// <summary>
        /// Lấy danh sách các đất nước trong database
        /// </summary>
        /// <returns></returns>
        List<Country> ListCountries();
    }
}