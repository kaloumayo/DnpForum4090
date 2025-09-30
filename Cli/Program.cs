using System;
using System.Threading.Tasks;
using RepositoryContracts;
using InMemoryRepositories;

// UI
using Cli.UI.ManageUsers;
using Cli.UI.ManagePosts;

// Top-level async "Main"
IUserRepository userRepo = new UserInMemoryRepository();
IPostRepository postRepo = new PostInMemoryRepository();
ICommentRepository commentRepo = new CommentInMemoryRepository();

// Kør hovedmenu
await RunMenuAsync();

async Task RunMenuAsync()
{
    while (true)
    {
        Console.WriteLine();
        Console.WriteLine("===== DNP Forum – CLI =====");
        Console.WriteLine("1) Opret bruger");
        Console.WriteLine("2) Opret post");
        Console.WriteLine("3) Vis alle brugere");
        Console.WriteLine("4) Vis alle posts");
        // (valgfrit senere) Console.WriteLine("5) Vis enkelt post");
        Console.WriteLine("0) Afslut");
        Console.Write("Vælg: ");

        string? input = Console.ReadLine();
        Console.WriteLine();

        switch (input)
        {
            case "1":
                await new CreateUserView(userRepo).RunAsync();
                break;

            case "2":
                await new CreatePostView(postRepo, userRepo).RunAsync();
                break;

            case "3":
                await new ListUsersView(userRepo).RunAsync();
                break;

            case "4":
                await new ListPostsView(postRepo, userRepo).RunAsync();
                break;

            case "0":
                Console.WriteLine("Farvel!");
                return;

            default:
                Console.WriteLine("Ugyldigt valg. Prøv igen.");
                break;
        }
    }
}