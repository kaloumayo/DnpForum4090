using Entities;
using RepositoryContracts;
using System.Linq;

namespace InMemoryRepositories;

public class CommentInMemoryRepository : ICommentRepository
{
    private readonly List<Comment> comments = new();

    public CommentInMemoryRepository()
    {
        comments.AddRange(new[]
        {
            new Comment { Id = 1, Body = "God pointe", UserId = 2, PostId = 1 }
        });
    }

    public Task<Comment> AddAsync(Comment comment)
    {
        comment.Id = comments.Any() ? comments.Max(c => c.Id) + 1 : 1;
        comments.Add(comment);
        return Task.FromResult(comment);
    }

    public Task UpdateAsync(Comment comment)
    {
        var existing = comments.SingleOrDefault(c => c.Id == comment.Id)
                       ?? throw new InvalidOperationException($"Comment '{comment.Id}' findes ikke");
        existing.Body = comment.Body;
        existing.UserId = comment.UserId;
        existing.PostId = comment.PostId;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var toRemove = comments.SingleOrDefault(c => c.Id == id)
                       ?? throw new InvalidOperationException($"Comment '{id}' findes ikke");
        comments.Remove(toRemove);
        return Task.CompletedTask;
    }

    public Task<Comment> GetSingleAsync(int id)
    {
        var c = comments.SingleOrDefault(c => c.Id == id)
                ?? throw new InvalidOperationException($"Comment '{id}' findes ikke");
        return Task.FromResult(c);
    }

    public IQueryable<Comment> GetMany() => comments.AsQueryable();
}