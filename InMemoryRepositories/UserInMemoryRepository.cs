using Entities;
using RepositoryContracts;
using System.Linq;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository
{
    private readonly List<User> users = new();

    public UserInMemoryRepository()
    {
        // Dummy data
        users.AddRange(new[]
        {
            new User { Id = 1, UserName = "Saleeman", Password = "pw1" },
            new User { Id = 2, UserName = "TestUser", Password = "pw2" },
        });
    }

    public Task<User> AddAsync(User user)
    {
        user.Id = users.Any() ? users.Max(u => u.Id) + 1 : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user)
    {
        var existing = users.SingleOrDefault(u => u.Id == user.Id)
                       ?? throw new InvalidOperationException($"User '{user.Id}' findes ikke");
        existing.UserName = user.UserName;
        existing.Password = user.Password;
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var toRemove = users.SingleOrDefault(u => u.Id == id)
                       ?? throw new InvalidOperationException($"User '{id}' findes ikke");
        users.Remove(toRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id)
    {
        var u = users.SingleOrDefault(u => u.Id == id)
                ?? throw new InvalidOperationException($"User '{id}' findes ikke");
        return Task.FromResult(u);
    }

    public IQueryable<User> GetMany() => users.AsQueryable();
}