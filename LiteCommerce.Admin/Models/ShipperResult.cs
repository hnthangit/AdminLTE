using LiteCommerce.DomainModels;
using System.Collections.Generic;

namespace LiteCommerce.Admin.Models
{
    public class ShipperResult : PaginationResult
    {
        public List<Shipper> Data;
    }
}