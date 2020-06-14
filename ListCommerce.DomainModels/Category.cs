namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Thể loại
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Mã thể loại
        /// </summary>
        public int CategoryID { get; set; }

        /// <summary>
        /// Tên thể loại
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Mô tả thể loại
        /// </summary>
        public string Description { get; set; }
    }
}