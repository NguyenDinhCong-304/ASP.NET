using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace NguyenDinhCong_2122110566.Models
{
    public class OrderDetail
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "OrderId không được để trống")]
        public long OrderId { get; set; }

        [Required(ErrorMessage = "ProductId không được để trống")]
        public long ProductId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
        public decimal Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải >= 1")]
        public int Qty { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount phải >= 0")]
        public decimal Amount { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Discount phải >= 0")]
        public decimal Discount { get; set; }

        [JsonIgnore]
        public Order? Order { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}