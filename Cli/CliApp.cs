using System;
using System.Threading.Tasks;
using RepositoryContracts;
using Cli.UI.ManageUsers;
using Cli.UI.ManagePosts;

namespace Cli;

public class CliApp
{
    private readonly IUserRepository _users;
    private readonly IPostRepository _posts;
    private readonly ICommentRepository _comments;

    public CliApp(IUserRepository users, IPostRepository posts, ICommentRepository comments)
    {
        _users = users;
        _posts = posts;
        _comments = comments;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== HOVEDMENU ===");
            Console.WriteLine("1) Manage Users");
            Console.WriteLine("2) Manage Posts");
            Console.WriteLine("0) Exit");
            Console.Write("Valg: ");
            string? choice = Console.ReadLine();   // <-- rettet

            switch (choice)
            {
                case "1":
                    await new ManageUsersView(_users).RunAsync();
                    break;
                case "2":
                    await new ManagePostsView(_posts, _users, _comments).RunAsync();
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