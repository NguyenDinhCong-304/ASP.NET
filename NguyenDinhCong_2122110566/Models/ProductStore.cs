using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductStore
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "ProductId không được để trống")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Giá nhập không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá nhập phải >= 0")]
        public decimal PriceRoot { get; set; }

        [Required(ErrorMessage = "Số lượng không được để trống")]
        [Range(0, int.MaxValue, ErrorMessage = "Số lượng phải >= 0")]
        public int Qty { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}