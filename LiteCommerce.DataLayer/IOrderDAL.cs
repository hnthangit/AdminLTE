using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface IOrderDAL
    {
        int Add(Order data);

        bool Update(Order data);

        bool Delete(Order data);

        Product Get(int orderID);

        List<Order> List(int page, int pageSize, string searchValue);
    }
}