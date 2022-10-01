using Blog.DataAccess.Dtos;
using Blog.DataAccess.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ardalis.GuardClauses;
using Blog.DataAccess.Commands;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediatR;
    public UserController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    [HttpPost("NewUser")]
    public async Task<ActionResult<User>> Insert(UserCreateDto dto)
    {
        Guard.Against.Null(dto, nameof(dto));
        return Ok(await _mediatR.Send(new InsertUserCommand(dto)));
    }

    [HttpPost("Login")]
    public async Task<ActionResult<string>> Login([FromBody] UserLoginDto dto)
    {
        Guard.Against.Null(dto, nameof(dto));
        return Ok(Content(await _mediatR.Send(new AuthenticateUserCommand(dto))));
    }
}
