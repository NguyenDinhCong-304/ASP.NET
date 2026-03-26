using System.ComponentModel.DataAnnotations;

namespace NguyenDinhCong_2122110566.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        public string Description { get; set; }

        // Navigation property for related products
        //public ICollection<Product> Products { get; set; }
    }
}
