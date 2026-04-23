using NguyenDinhCong_2122110566.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class Topic
{
    public long Id { get; set; }

    [Required, StringLength(200)]
    public string Name { get; set; }

    [Required, StringLength(200)]
    public string Slug { get; set; }

    [Range(0, int.MaxValue)]
    public int SortOrder { get; set; } = 0;

    [StringLength(500)]
    public string? Description { get; set; }

    public int Status { get; set; } = 1; // 1 = Active, 0 = Hidden

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public ICollection<Post>? Posts { get; set; }
}