using System.ComponentModel.DataAnnotations;

namespace NguyenDinhCong_2122110566.Models
{
    public class Setting
    {
        public long Id { get; set; }

        [Required, StringLength(200)]
        public string SiteName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, Phone, StringLength(20)]
        public string Phone { get; set; }

        [Required, StringLength(300)]
        public string Address { get; set; }

        public int Status { get; set; } = 1; // 1 = active, 0 = hidden

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}