using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DataAccess.Entities;

[Table("tags")]
public sealed class Tag : BaseEntity<Guid>
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = null!;
}