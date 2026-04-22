using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductImage
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "ProductId không được để trống")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Đường dẫn hình ảnh không được để trống")]
        [StringLength(300, ErrorMessage = "Đường dẫn hình ảnh tối đa 300 ký tự")]
        public string Image { get; set; }

        [StringLength(200, ErrorMessage = "Tiêu đề tối đa 200 ký tự")]
        public string? Title { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}