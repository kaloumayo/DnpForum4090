using System;
using System.Linq;
using System.Threading.Tasks;
using RepositoryContracts;

namespace Cli.UI.ManageUsers;

public class ListUsersView
{
    private readonly IUserRepository _users;

    public ListUsersView(IUserRepository users) => _users = users;

    public Task RunAsync()
    {
        var all = _users.GetMany().ToList();
        if (!all.Any())
        {
            Console.WriteLine("Ingen brugere fundet.");
            return Task.CompletedTask;
        }

        Console.WriteLine("Brugere:");
        foreach (var u in all.OrderBy(u => u.Id))
            Console.WriteLine($"- [{u.Id}] {u.UserName}");

        return Task.CompletedTask;
    }
}