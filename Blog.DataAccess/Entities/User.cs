using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DataAccess.Entities;

[Table("users")]
public sealed class User : BaseEntity<Guid> 
{
    [MaxLength(200)]
    public string? ImageURI { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = null!;

    [Required]
    public Gender Gender { get; set; }

    [Required]
    public Int16 Age { get; set; } = default!;

    [Required]
    [EmailAddress]
    [MaxLength(200)]
    public string Email { get; set; } = null!;

    [MaxLength(350)]
    public string? Description { get; set; }

    [MaxLength(6)]
    public string? CountryCode{ get; set; }

    [Phone]
    [MaxLength(50)]
    public string? Phone { get; set; }
    
    [ForeignKey("ArticleId")]
    public List<Article> Articles { get; set; } = new();

    [Required]
    public byte[] PasswordSalt { get; set; } = null!;

    [Required]
    public byte[] PasswordHash { get; set; } = null!;
}

public enum Gender
{
    MALE,
    FEMALE,
    OTHER,
}