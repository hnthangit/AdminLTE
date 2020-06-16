using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    /// <summary>
    /// Interface Order Detail
    /// </summary>
    public interface IOrderDetailDAL
    {
        /// <summary>
        /// Thêm chi tiết cho một đơn hàng
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        int Add(OrderDetail data);

        bool Update(OrderDetail data);

        bool Delete(int[] orderDetailIDs);

        OrderDetail Get(int orderDetailID);

        List<OrderDetail> List(int page, int pageSize, string searchValue);

        List<OrderDetail> List(int orderID);
    }
}