using Entities;
using RepositoryContracts;
using System.Linq;

namespace InMemoryRepositories;

public class PostInMemoryRepository : IPostRepository
{
    private readonly List<Post> posts = new();

    public PostInMemoryRepository()
    {
        posts.AddRange(new[]
        {
            new Post { Id = 1, Title = "Velkommen", Body = "Første post", UserId = 1 },
            new Post { Id = 2, Title = "Regler", Body = "Læs reglerne", UserId = 2 },
        });
    }

    public Task<Post> AddAsync(Post post)
    {
        post.Id = posts.Any() ? posts.Max(p => p.Id) + 1 : 1;
        posts.Add(post);
        return Task.FromResult(post);
    }

    public Task UpdateAsync(Post post)
    {
        var existing = posts.SingleOrDefault(p => p.Id == post.Id)
                       ?? throw new InvalidOperationException($"Post '{post.Id}' findes ikke");
        existing.Title = post.Title;
        existing.Body = post.Body;
        existing.UserId = post.UserId;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var toRemove = posts.SingleOrDefault(p => p.Id == id)
                       ?? throw new InvalidOperationException($"Post '{id}' findes ikke");
        posts.Remove(toRemove);
        return Task.CompletedTask;
    }

    public Task<Post> GetSingleAsync(int id)
    {
        var p = posts.SingleOrDefault(p => p.Id == id)
                ?? throw new InvalidOperationException($"Post '{id}' findes ikke");
        return Task.FromResult(p);
    }

    public IQueryable<Post> GetMany() => posts.AsQueryable();
}