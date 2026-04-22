using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class Brand
    {
        [Key]
        public long Id { get; set; }

        [Required(ErrorMessage = "Tên thương hiệu không được để trống")]
        [StringLength(200, ErrorMessage = "Tên thương hiệu tối đa 200 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Slug không được để trống")]
        [StringLength(200)]
        public string Slug { get; set; }

        [Required(ErrorMessage = "Logo không được để trống")]
        public string Logo { get; set; }

        [StringLength(500, ErrorMessage = "Mô tả tối đa 500 ký tự")]
        public string? Description { get; set; }

        [StringLength(100, ErrorMessage = "Tên quốc gia tối đa 100 ký tự")]
        public string? Country { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public int Status { get; set; }

        [JsonIgnore]
        public ICollection<Product>? Products { get; set; }
    }
}