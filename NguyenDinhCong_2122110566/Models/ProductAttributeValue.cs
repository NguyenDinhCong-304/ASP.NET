using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductAttributeValue
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "ProductId không được để trống")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "AttributeId không được để trống")]
        public long AttributeId { get; set; }

        [Required(ErrorMessage = "Giá trị thuộc tính không được để trống")]
        [StringLength(200, ErrorMessage = "Giá trị tối đa 200 ký tự")]
        public string Value { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }

        [JsonIgnore]
        public ProductAttribute? Attribute { get; set; }
    }
}