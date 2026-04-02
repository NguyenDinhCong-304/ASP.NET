using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NguyenDinhCong_2122110566.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(200, ErrorMessage = "Name cannot exceed 200 characters.")]
        public string Name { get; set; }

        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, 99999999.99, ErrorMessage = "Price must be non-negative.")]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Stock must be non-negative.")]
        public int Stock { get; set; }

        // Foreign key to Category
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]

        // Image URL for the product (optional)
        //[StringLength(2048, ErrorMessage = "Image URL cannot exceed 2048 characters.")]
        //[Url(ErrorMessage = "Image must be a valid URL.")]
        //public string Image { get; set; }
        public Category? Category { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
