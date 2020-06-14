namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Sản phẩm
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Tên sản phẩm
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        public int SupplierID { get; set; }

        /// <summary>
        /// Mã thể loại
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Số lượng mỗi đơn vị
        /// </summary>
        public string QuantityPerUnit { get; set; }

        /// <summary>
        /// Đơn giá
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string Descriptions { get; set; }

        /// <summary>
        /// Hình ảnh sản phẩm
        /// </summary>
        public string PhotoPath { get; set; }
    }
}