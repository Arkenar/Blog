using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DataAccess.Entities;

[Table("articles")]
public sealed class Article: BaseEntity<Guid>
{
    [Required]
    [MaxLength(200)]
    public string Title {get; set;} = null!;

    [Column("text")]
    public string Content { get; set; } = null!;

    [Required]
    public User Owner { get; set; } = new();

    [ForeignKey("TagId")]
    public List<Tag> Tags { get; set; } = new();
}
