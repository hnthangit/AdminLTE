using System;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Thông tin về nhà cung cấp hằng hóa
    /// </summary>
    public class Supplier
    {
        /// <summary>
        /// Mã nhà cung cấp hàng hóa
        /// </summary>
        public Int32 SupplierID { get; set; }

        /// <summary>
        /// Tên công ty
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Tên người liên hệ
        /// </summary>
        public string ContactName { get; set; }

        /// <summary>
        /// Tên liên hệ
        /// </summary>
        public string ContactTitle { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Tên thành phố
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Tên đất nước
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

        /// <summary>
        /// Địa chỉ web
        /// </summary>
        public string HomePage { get; set; }
    }
}