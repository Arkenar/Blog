using Blog.DataAccess.Data;
using Z.EntityFramework.Plus;
using MediatR;

namespace Blog.DataAccess.Commands;

public sealed record RemoveTagCommand(string Name) : IRequest<bool>;

public sealed class RemoveTagHandler : IRequestHandler<RemoveTagCommand, bool>
{
    private readonly BlogDbContext _db;
    public RemoveTagHandler(BlogDbContext db)
    {
        _db = db;
    }

    public async Task<bool> Handle(RemoveTagCommand request, CancellationToken cancellationToken)
    {
        return await _db.Tags.Where(p => p.Name == request.Name).DeleteFromQueryAsync(cancellationToken) > 0;
    }

}