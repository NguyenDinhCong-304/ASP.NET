using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Menu
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên menu không được để trống")]
        [StringLength(200, ErrorMessage = "Tên menu tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Link không được để trống")]
        [StringLength(500)]
        public string Link { get; set; }

        [Required(ErrorMessage = "Type không được để trống")]
        [StringLength(50)]
        public string Type { get; set; }

        public long? ParentId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "SortOrder phải >= 0")]
        public int SortOrder { get; set; }

        public long? TableId { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Menu? Parent { get; set; }

        [JsonIgnore]
        public ICollection<Menu>? Children { get; set; }
    }
}
