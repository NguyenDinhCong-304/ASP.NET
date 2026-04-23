using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductStore
    {
        public long Id { get; set; }

        [Required]
        public long ProductId { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal PriceRoot { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Qty { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}