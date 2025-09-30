using System;
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
        string userName = Console.ReadLine() ?? "";

        Console.Write("Adgangskode: ");
        string password = Console.ReadLine() ?? "";

        if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
        {
            Console.WriteLine("Brugernavn og adgangskode må ikke være tomme.");
            return;
        }

        User created = await _users.AddAsync(new User { UserName = userName, Password = password });
        Console.WriteLine($"✔ Oprettet bruger Id={created.Id}, Navn={created.UserName}");
    }
}