using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Product
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "BrandId không được để trống")]
        public long BrandId { get; set; }

        [Required(ErrorMessage = "CategoryId không được để trống")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được để trống")]
        [StringLength(300, ErrorMessage = "Tên sản phẩm tối đa 300 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug không được để trống")]
        [StringLength(300)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Thumbnail không được để trống")]
        public string Thumbnail { get; set; }

        [Required(ErrorMessage = "Nội dung sản phẩm không được để trống")]
        public string Content { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal Price { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Brand? Brand { get; set; }

        [JsonIgnore]
        public Category? Category { get; set; }

        [JsonIgnore]
        public ICollection<ProductImage>? Images { get; set; }

        [JsonIgnore]
        public ICollection<ProductSale>? Sales { get; set; }

        [JsonIgnore]
        public ICollection<ProductAttributeValue>? Attributes { get; set; }
    }
}