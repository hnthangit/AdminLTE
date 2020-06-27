using System.ComponentModel.DataAnnotations;

namespace LiteCommerce.DomainModels
{
    public class ResetPasswordModel
    {
        /// <summary>
        /// Mật khẩu mới
        /// </summary>
        [Required(ErrorMessage = "mật khẩu mới là bắt buộc")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Nhập lại mật khẩu mới
        /// </summary>
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu nhập lại không trùng nhau! ")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Mã đặt lại mật khẩu
        /// </summary>
        [Required]
        public string ResetCode { get; set; }
    }
}