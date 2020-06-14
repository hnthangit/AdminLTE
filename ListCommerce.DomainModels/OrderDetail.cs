using System;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Chi tiết đơn hàng
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// Mã đơn hàng
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Số lượng
        /// </summary>
        public Int16 Quantity { get; set; }

        /// <summary>
        /// Giảm giá
        /// </summary>
        public Single Discount { get; set; }
    }
}