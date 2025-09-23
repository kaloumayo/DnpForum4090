using System;
using System.Threading.Tasks;
using RepositoryContracts;

namespace Cli.UI.ManageUsers;

public class ManageUsersView
{
    
    private readonly IUserRepository _users;

    public ManageUsersView(IUserRepository users) => _users = users;

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage Users ===");
            Console.WriteLine("1) Create user");
            Console.WriteLine("2) List users");
            Console.WriteLine("0) Back");
            Console.Write("Valg: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreateUserView(_users).RunAsync();
                    break;

                case "2":
                    await new ListUsersView(_users).RunAsync();
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Ugyldigt valg.");
                    break;
            }
        }
    }
}