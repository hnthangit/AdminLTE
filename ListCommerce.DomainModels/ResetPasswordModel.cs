using System.ComponentModel.DataAnnotations;

namespace LiteCommerce.DomainModels
{
    public class ResetPasswordModel
    {
        [Required(ErrorMessage = "mật khẩu mới là bắt buộc")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Mật khẩu mới và mật khẩu nhập lại không trùng nhau! ")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ResetCode { get; set; }
    }
}