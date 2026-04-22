using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductAttribute
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên thuộc tính không được để trống")]
        [StringLength(200, ErrorMessage = "Tên thuộc tính tối đa 200 ký tự")]
        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<ProductAttributeValue>? ProductAttributes { get; set; }
    }
}