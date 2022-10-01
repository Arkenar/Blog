using System.ComponentModel.DataAnnotations;
using Blog.DataAccess.Entities;

namespace Blog.DataAccess.Dtos;

public sealed record UserCreateDto
(
    string FirstName, 
    string LastName, 
    string Email, 
    string CountryCode,
    string Phone, 
    string Password, 
    Int16 Age, 
    Gender Gender, 
    string? Description
);


public sealed class UserLoginDto
{
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = null!;

    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;
}