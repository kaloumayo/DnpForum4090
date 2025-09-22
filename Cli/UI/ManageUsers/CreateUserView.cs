using System;
using System.Linq;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace Cli.UI.ManageUsers;

public class CreateUserView
{
    private readonly IUserRepository _users;

    public CreateUserView(IUserRepository users) => _users = users;

    public async Task RunAsync()
    {
        Console.Write("Brugernavn: ");
        var name = Console.ReadLine() ?? "";
        Console.Write("Password: ");
        var pw = Console.ReadLine() ?? "";

        // simpelt dup-tjek
        bool exists = _users.GetMany()
            .Any(u => u.UserName.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (exists)
        {
            Console.WriteLine("Brugernavn er optaget.");
            return;
        }

        var created = await _users.AddAsync(new User { UserName = name, Password = pw });
        Console.WriteLine($"✔ Oprettet bruger med Id={created.Id}");
    }
}