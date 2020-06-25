namespace LiteCommerce.Admin
{
    /// <summary>
    /// Định nghĩa danh sách các Role của user
    /// </summary>
    public class WebUserRoles
    {
        /// <summary>
        /// Không xác định
        /// </summary>
        public const string ANONYMOUS = "anonymous";

        /// <summary>
        /// Nhân viên
        /// </summary>
        public const string STAFF = "staff";

        /// <summary>
        /// Quản trị hệ thống
        /// </summary>
        public const string ADMINISTRATOR = "Adminstrator";

        /// <summary>
        /// Nhân viên bán hàng
        /// </summary>
        public const string SALEMAN = "Saleman";

        /// <summary>
        /// Quản lý dữ liệu
        /// </summary>
        public const string ACCOUNTANT = "Accountant";

        public const string FULL = "Saleman,Accountant,Adminstrator";
    }
}