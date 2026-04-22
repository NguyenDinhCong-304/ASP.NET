using System.ComponentModel.DataAnnotations;

namespace NguyenDinhCong_2122110566.Models
{
    public class Setting
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên website không được để trống")]
        [StringLength(200, ErrorMessage = "Tên website tối đa 200 ký tự")]
        public string SiteName { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(20)]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(300, ErrorMessage = "Địa chỉ tối đa 300 ký tự")]
        public string Address { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}