using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Order
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "UserId không được để trống")]
        public long UserId { get; set; }

        [Required(ErrorMessage = "Tên người nhận không được để trống")]
        [StringLength(200, ErrorMessage = "Tên tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số điện thoại không được để trống")]
        [Phone(ErrorMessage = "Số điện thoại không hợp lệ")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Địa chỉ không được để trống")]
        [StringLength(500, ErrorMessage = "Địa chỉ tối đa 500 ký tự")]
        public string Address { get; set; }

        [StringLength(1000, ErrorMessage = "Ghi chú tối đa 1000 ký tự")]
        public string? Note { get; set; }

        [Range(0, 5, ErrorMessage = "Status không hợp lệ")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public User? User { get; set; }

        [JsonIgnore]
        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
