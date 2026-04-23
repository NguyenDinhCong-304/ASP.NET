using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using NguyenDinhCong_2122110566.Enums;

namespace NguyenDinhCong_2122110566.Models
{
    public class ProductSale
    {
        public long Id { get; set; }

        [Required(ErrorMessage = "ProductId không được để trống")]
        public long ProductId { get; set; }

        [Required(ErrorMessage = "Giá khuyến mãi không được để trống")]
        [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải >= 0")]
        public decimal PriceSale { get; set; }

        [Required(ErrorMessage = "Ngày bắt đầu không được để trống")]
        public DateTime DateBegin { get; set; }

        [Required(ErrorMessage = "Ngày kết thúc không được để trống")]
        public DateTime DateEnd { get; set; }

        [Range(0, 1, ErrorMessage = "Status chỉ được 0 hoặc 1")]
        public ProductSaleStatus Status { get; set; } = ProductSaleStatus.Active;

        [JsonIgnore]
        public Product? Product { get; set; }
    }
}