using Blog.DataAccess.Data;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Blog.DataAccess.Entities;
using Ardalis.GuardClauses;
using Blog.DataAccess.Commands;

namespace Blog.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly IMediator _mediatR;
    public TagController(IMediator mediatR)
    {
        _mediatR = mediatR;
    }

    [HttpPost("InsertOne")]
    public async Task<ActionResult<Tag>> Insert(string name)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return Ok(await _mediatR.Send(new InsertTagCommand(name)));
    }


    [HttpDelete("DeleteOne")]
    public async Task<ActionResult<bool>> Remove(string name)
    {
        Guard.Against.NullOrWhiteSpace(name, nameof(name));
        return Ok(await _mediatR.Send(new RemoveTagCommand(name)));
    }
}
