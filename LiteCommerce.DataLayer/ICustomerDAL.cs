using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.DataLayer
{
    public interface ICustomerDAL
    {
        int Add(Customer data);

        bool Update(Customer data);

        bool Delete(Customer data);

        Customer Get(int customerID);

        List<Customer> List(int page, int pageSize, string searchValue);
    }
}