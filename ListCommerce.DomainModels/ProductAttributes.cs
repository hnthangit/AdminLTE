namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Chi tiết sản phẩm
    /// </summary>
    public class ProductAttributes
    {
        /// <summary>
        /// Mã chi tiết sản phẩm
        /// </summary>
        public long AttributeID { get; set; }

        /// <summary>
        /// Mã sản phẩm
        /// </summary>
        public int ProductID { get; set; }

        /// <summary>
        /// Tên chi tiết
        /// </summary>
        public string AttributeName { get; set; }

        /// <summary>
        /// Nội dung chi tiết
        /// </summary>
        public string AttributeValues { get; set; }

        /// <summary>
        /// Mã hiển thị đặt hàng
        /// </summary>
        public int DisplayOrder { get; set; }
    }
}