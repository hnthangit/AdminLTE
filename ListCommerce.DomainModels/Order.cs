using System;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Đơn đặt hàng
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Mã đơn
        /// </summary>
        public int OrderID { get; set; }

        /// <summary>
        /// Mã khách hàng
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Ngày đặt
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Ngày bắt buộc
        /// </summary>
        public DateTime RequiredDate { get; set; }

        /// <summary>
        /// Ngày giao hàng
        /// </summary>
        public DateTime ShippedDate { get; set; }

        /// <summary>
        /// Mã người giao hàng
        /// </summary>
        public int ShipperID { get; set; }

        /// <summary>
        /// Cân nặng
        /// </summary>
        public decimal Freight { get; set; }

        /// <summary>
        /// Địa chỉ giao hàng
        /// </summary>
        public string ShipAddress { get; set; }

        /// <summary>
        /// Địa chỉ thành phố giao hàng
        /// </summary>
        public string ShipCity { get; set; }

        /// <summary>
        /// Địa chỉ đất nước giao hàng
        /// </summary>
        public string ShipCountry { get; set; }
    }
}