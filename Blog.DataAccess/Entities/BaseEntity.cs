using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.DataAccess.Entities;

public abstract class BaseEntity<TId> where TId : notnull, new()
{
    [Required]
    [Key]
    public TId Id { get; set; } = new();

    public DateTimeOffset CreatedAt => DateTimeOffset.UtcNow;
}