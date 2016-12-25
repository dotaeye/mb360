using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MB.Models
{
    // 用作 AccountController 操作的参数的模型。

    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "外部访问令牌")]
        public string ExternalAccessToken { get; set; }
    }

    public class ChangePasswordBindingModel
    {
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "当前密码")]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "确认新密码")]
        [Compare("NewPassword", ErrorMessage = "新密码和确认密码不匹配。")]
        public string ConfirmPassword { get; set; }
    }

    public class RegisterBindingModel
    {
        [Required]
        [Display(Name = "手机号码")]
        public string Email { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "密码")]
        public string Password { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "请输入{2}位手机短信证码", MinimumLength = 6)]
        [Display(Name = "验证码")]
        public string SmsCode { get; set; }

        [Required]
        [Display(Name = "推荐人手机号")]
        public string RefPhone { get; set; }

    }

    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "电子邮件")]
        public string Email { get; set; }
    }

    public class RemoveLoginBindingModel
    {
        [Required]
        [Display(Name = "登录提供程序")]
        public string LoginProvider { get; set; }

        [Required]
        [Display(Name = "提供程序密钥")]
        public string ProviderKey { get; set; }
    }

    public class SetPasswordBindingModel
    {
        [Required]
        [Display(Name = "手机号码")]
        public string UserName { get; set; }

        [Required]
        [StringLength(6, ErrorMessage = "请输入{2}位手机短信证码", MinimumLength = 6)]
        [Display(Name = "验证码")]
        public string SmsCode { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "{0} 必须至少包含 {2} 个字符。", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "新密码")]
        public string NewPassword { get; set; }

    }
}
