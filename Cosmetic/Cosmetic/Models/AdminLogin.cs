using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetic.Models
{
    public class AdminLogin
    {
        [Display(Name = "Tài khoản")]
        [Required(ErrorMessage = "Phải nhập")]
        [MaxLength(20, ErrorMessage = "Tối đa 20 kí tự")]
        public string MaNv { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Phải nhập")]
        [MaxLength(50, ErrorMessage = "Tối đa 50 kí tự")]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }
    }
}
