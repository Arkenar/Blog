using Blog.DataAccess.Data;
using Blog.DataAccess.Dtos;
using Blog.DataAccess.Entities;
using Z.EntityFramework.Plus;
using MediatR;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using Blog.DataAccess.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Blog.DataAccess.Commands;

public sealed record AuthenticateUserCommand(UserLoginDto Dto) : IRequest<string>;

public sealed class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly BlogDbContext _db;
    private readonly IAuthenticateUser _auth;
    private readonly IConfiguration _config;
    public AuthenticateUserHandler (BlogDbContext db, IAuthenticateUser auth, IConfiguration config)
    {
        _db = db;
        _auth = auth;
        _config = config;
    }

    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.Where(p => p.Email == request.Dto.Email)
                    .Select(p => new User
                                {
                                    Email = p.Email, 
                                    PasswordHash = p.PasswordHash, 
                                    PasswordSalt = p.PasswordSalt
                                })
                    .SingleOrDefaultAsync(cancellationToken);

        Guard.Against.Null(user, nameof(user));

        var verify = _auth.VerifyPassword(request.Dto.Password, user.PasswordHash, user.PasswordSalt);

        if(!verify) throw new 
            NotImplementedException("The password or email is invalid. Make sure your credentials are correct");

        return _auth.CreateToken(request.Dto.Email, _config);
    }
}