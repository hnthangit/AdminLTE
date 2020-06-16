using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationResult : PaginationResult
    {
        public List<Order> Data;
    }
}
