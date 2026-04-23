using NguyenDinhCong_2122110566.Enums;
using NguyenDinhCong_2122110566.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Product
{
    public long Id { get; set; }

    [Required]
    public long BrandId { get; set; }

    [Required]
    public long CategoryId { get; set; }

    [Required, StringLength(300)]
    public string Name { get; set; }

    [Required, StringLength(300)]
    public string Slug { get; set; }

    [Required]
    public string Thumbnail { get; set; }

    [Required]
    public string Content { get; set; }

    [StringLength(500)]
    public string? Description { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public ProductStatus Status { get; set; } = ProductStatus.Active;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

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