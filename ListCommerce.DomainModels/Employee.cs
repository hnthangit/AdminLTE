using System;

namespace LiteCommerce.DomainModels
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public int EmployeeID { get; set; }

        /// <summary>
        /// Họ
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Tên
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Chức vụ
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Ngày thuê làm việc
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Địa chỉ email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Địa chỉ nhà
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
        /// Số điện thoại nhà
        /// </summary>
        public string HomePhone { get; set; }

        /// <summary>
        /// Ghi chú
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        public string PhotoPath { get; set; }

        /// <summary>
        /// Mật khẩu
        /// </summary>
        public string Password { get; set; }
    }
}