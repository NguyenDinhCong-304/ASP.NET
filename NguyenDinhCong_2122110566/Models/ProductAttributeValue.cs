using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ProductAttributeValue
{
    public long Id { get; set; }

    [Required]
    public long ProductId { get; set; }

    [Required]
    public long AttributeId { get; set; }

    [Required, StringLength(200)]
    public string Value { get; set; }

    // navigation
    [JsonIgnore]
    public Product? Product { get; set; }

    [JsonIgnore]
    public ProductAttribute? Attribute { get; set; }
}