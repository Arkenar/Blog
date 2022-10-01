using Ardalis.GuardClauses;
using Blog.DataAccess.Data;
using Blog.DataAccess.Dtos;
using Blog.DataAccess.Entities;
using Blog.DataAccess.Interfaces;
using Blog.DataAccess.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.DataAccess.Commands;

public sealed record InsertUserCommand(UserCreateDto Dto) : IRequest<User>;

public sealed class InsertUserHandler : IRequestHandler<InsertUserCommand, User>
{
    private readonly BlogDbContext _db;
    private readonly IPhoneValidation _phone;
    private readonly IAuthenticateUser _auth;
    public InsertUserHandler(BlogDbContext db, IPhoneValidation phone, IAuthenticateUser auth)
    {
        _db = db;
        _phone = phone;
        _auth = auth;
    }

    public async Task<User> Handle(InsertUserCommand request, CancellationToken cancellationToken)
    {
        Guard.Against.Null(request.Dto, nameof(request.Dto));
        _phone.PhoneValidAndFormatted(request.Dto.CountryCode, request.Dto.Phone, out string validPrefix, out string validNbr);
        _auth.CreatePasswordHashAndSalt(request.Dto.Password, out byte[] passwordHash, out byte[] passwordSalt);
        
        User user = new()
        {
            FirstName = request.Dto.FirstName,
            LastName = request.Dto.LastName,
            Email = request.Dto.Email,
            Age = request.Dto.Age,
            Gender = request.Dto.Gender,
            Description = request.Dto.Description,
            CountryCode = validPrefix,
            Phone = validNbr,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
        };
        
        _db.Users.Attach(user).State = EntityState.Added;
        await _db.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(user);
    }
}