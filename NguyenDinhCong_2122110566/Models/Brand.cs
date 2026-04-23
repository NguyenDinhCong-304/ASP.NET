using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Brand
    {
        [Key]
        public long Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; }

        [Required, StringLength(200)]
        public string Slug { get; set; }

        [Required]
        public string Logo { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        public int Status { get; set; } = 1; // 1 active, 0 hidden

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}