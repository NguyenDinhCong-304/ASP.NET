using NguyenDinhCong_2122110566.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class User
{
    public long Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, Phone, StringLength(20)]
    public string Phone { get; set; }

    [Required, StringLength(100)]
    public string Username { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public string Roles { get; set; } = "User";

    public string? Avatar { get; set; }

    public int Status { get; set; } = 1; // 1 active, 0 hidden

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public ICollection<Order>? Orders { get; set; }
}