using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Topic
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên chủ đề không được để trống")]
        [StringLength(200, ErrorMessage = "Tên chủ đề tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug không được để trống")]
        [StringLength(200)]
        public string Slug { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "SortOrder phải >= 0")]
        public int SortOrder { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public ICollection<Post>? Posts { get; set; }
    }
}