using NguyenDinhCong_2122110566.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class ProductAttribute
{
    public long Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; }

    // navigation
    [JsonIgnore]
    public ICollection<ProductAttributeValue>? Values { get; set; }
}