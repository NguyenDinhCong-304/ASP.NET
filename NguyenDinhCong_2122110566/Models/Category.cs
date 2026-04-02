using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        // Navigation property to related products
        [JsonIgnore]
        public ICollection<Product> Products { get; set; } = new List<Product>();

        // Concurrency token for optimistic concurrency control
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
