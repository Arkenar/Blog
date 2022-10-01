namespace Blog.DataAccess.Interfaces;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;

public interface IAuthenticateUser
{
    public void CreatePasswordHashAndSalt(string password, out byte[] passwordHash, out byte[] passwordSalt);
    public bool VerifyPassword(string password, byte[] passwordHash, byte[] passwordSalt);
    public string CreateToken(string email, IConfiguration _configuration);
}