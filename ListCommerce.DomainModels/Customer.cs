namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Khách hàng
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// Mã Khách hàng
        /// </summary>
        public string CustomerID { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Tên liên lạc
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Chi tiết liên lạc
        /// </summary>
        public string ContactTitle { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Đất nước
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Số fax
        /// </summary>
        public string Fax { get; set; }
    }
}