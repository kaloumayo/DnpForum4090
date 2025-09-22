using System.Linq;
using RepositoryContracts;

namespace Cli.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _users;

    public ListUsersView(IUserRepository users) => _users = users;

    public Task RunAsync()
    {
        Console.WriteLine("\n[Users]");
        var list = _users.GetMany().OrderBy(u => u.Id).ToList();
        if (list.Count == 0) Console.WriteLine("(ingen)");
        foreach (var u in list) Console.WriteLine($"({u.Id}) {u.UserName}");
        return Task.CompletedTask;
    }
}