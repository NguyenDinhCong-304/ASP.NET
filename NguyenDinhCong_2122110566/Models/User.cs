using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class User
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(200, ErrorMessage = "Tên tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Username không được để trống")]
        [StringLength(100, ErrorMessage = "Username tối đa 100 ký tự")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password không được để trống")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Password tối thiểu 6 ký tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Role không được để trống")]
        [StringLength(50)]
        public string Roles { get; set; }

        public string? Avatar { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<Order>? Orders { get; set; }
    }
}