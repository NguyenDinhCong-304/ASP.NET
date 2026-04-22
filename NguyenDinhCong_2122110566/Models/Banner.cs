using System.ComponentModel.DataAnnotations;

namespace NguyenDinhCong_2122110566.Models
{
    public class Banner
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên banner không được để trống")]
        [StringLength(200, ErrorMessage = "Tên banner tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        public string Image { get; set; }

        [Url(ErrorMessage = "Link phải đúng định dạng URL")]
        public string? Link { get; set; }

        [Required(ErrorMessage = "Vị trí banner không được để trống")]
        [StringLength(100)]
        public string Position { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "SortOrder phải lớn hơn hoặc bằng 0")]
        public int SortOrder { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}
