using Blog.DataAccess.Data;
using Blog.DataAccess.Entities;
using MediatR;

namespace Blog.DataAccess.Commands;

public sealed record InsertTagCommand(string Name) : IRequest<Tag>;

public sealed class InsertTagHandler : IRequestHandler<InsertTagCommand, Tag>
{
    private readonly BlogDbContext _db;
    public InsertTagHandler(BlogDbContext db)
    {
        _db = db;
    }

    public async Task<Tag> Handle(InsertTagCommand request, CancellationToken cancellationToken)
    {
        Tag tag = new() {Name = request.Name};
        _db.Attach(tag);
        await _db.SaveChangesAsync(cancellationToken);

        return await Task.FromResult(tag);
    }

}