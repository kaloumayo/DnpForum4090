using Entities;
using RepositoryContracts;

namespace FileRepositories;

public sealed class UserFileRepository : IUserRepository
{
    private readonly string _path;

    public UserFileRepository(string? baseDir = null)
    {
        // Gemmer i build-output under .../bin/Debug/net9.0/data/users.json
        baseDir ??= Path.Combine(AppContext.BaseDirectory, "data");
        _path = Path.Combine(baseDir, "users.json");
    }

    public async Task<User> AddAsync(User user)
    {
        var users = await FileDb.LoadAsync<User>(_path);

        // Tildel Id hvis mangler
        if (user.Id == 0)
            user.Id = users.Count == 0 ? 1 : users.Max(u => u.Id) + 1;

        users.Add(user);
        await FileDb.SaveAsync(_path, users);
        return user;
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<User> GetMany()
    {
        // CLI kalder denne synkront – vi loader synkront via GetAwaiter
        return FileDb.LoadAsync<User>(_path).GetAwaiter().GetResult();
    }

    public async Task<User?> GetSingleAsync(int id)
    {
        var users = await FileDb.LoadAsync<User>(_path);
        return users.FirstOrDefault(u => u.Id == id);
    }

    IQueryable<User> IUserRepository.GetMany()
    {
        throw new NotImplementedException();
    }
}