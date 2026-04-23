using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NguyenDinhCong_2122110566.Enums;

namespace NguyenDinhCong_2122110566.Models
{
    public class Post
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "TopicId không được để trống")]
        public long TopicId { get; set; }

        [Required(ErrorMessage = "Tiêu đề không được để trống")]
        [StringLength(300, ErrorMessage = "Tiêu đề tối đa 300 ký tự")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Slug không được để trống")]
        [StringLength(300)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Hình ảnh không được để trống")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Nội dung không được để trống")]
        public string Content { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public PostStatus Status { get; set; } = PostStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Topic? Topic { get; set; }
    }
}