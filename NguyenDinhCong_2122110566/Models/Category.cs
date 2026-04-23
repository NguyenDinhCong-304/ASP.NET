using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Slug { get; set; }

        [Required]
        public string Image { get; set; }

        public long? ParentId { get; set; } // category cha

        [Range(0, int.MaxValue)]
        public int SortOrder { get; set; } = 0;

        [StringLength(500)]
        public string? Description { get; set; }

        public int Status { get; set; } = 1; // 1 active, 0 hidden

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // navigation

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}