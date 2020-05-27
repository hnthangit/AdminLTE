using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface ShipperDAL
    /// </summary>
    public interface IShipperDAL
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(Shipper data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Shipper data);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipperIDs"></param>
        /// <returns></returns>
        bool Delete(int[] shipperIDs);

        /// <summary>
        ///
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        Shipper Get(int shipperID);

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Shipper> List(int page, int pageSize, string searchValue);

        /// <summary>
        ///
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);
    }
}