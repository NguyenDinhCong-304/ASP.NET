using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Contact
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "UserId không được để trống")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(200, ErrorMessage = "Tên tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        [StringLength(1000, ErrorMessage = "Nội dung tối đa 1000 ký tự")]
        public string Content { get; set; }

        public string? ReplyId { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        [JsonIgnore]
        public User? User { get; set; }
    }
}
