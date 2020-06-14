namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Thông tin người sử dụng
    /// </summary>
    public class UserAccount
    {
        /// <summary>
        /// ID của user
        /// </summary>
        public string UserID { get; set; }

        /// <summary>
        /// Full name
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Đường dẫn ảnh
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Tên nhóm quản trị
        /// </summary>
        public string GroupName { get; set; }
    }
}