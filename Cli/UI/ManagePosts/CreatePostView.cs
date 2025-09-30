using System;
using System.Threading.Tasks;
using Entities;
using RepositoryContracts;

namespace Cli.UI.ManagePosts;

public class CreatePostView
{
    private readonly IPostRepository _posts;
    private readonly IUserRepository _users;

    public CreatePostView(IPostRepository posts, IUserRepository users)
    {
        _posts = posts;
        _users = users;
    }

    public async Task RunAsync()
    {
        Console.Write("Titel: ");
        string title = Console.ReadLine() ?? "";

        Console.Write("Brødtekst: ");
        string body = Console.ReadLine() ?? "";

        Console.Write("Forfatter UserId: ");
        bool ok = int.TryParse(Console.ReadLine(), out int userId);
        if (!ok)
        {
            Console.WriteLine("Ugyldigt UserId.");
            return;
        }

        // Tjek at brugeren findes
        var user = await _users.GetSingleAsync(userId);
        if (user == null)
        {
            Console.WriteLine("Ingen bruger fundet med det ID.");
            return;
        }

        // Opret posten
        var created = await _posts.AddAsync(new Post
        {
            Title = title,
            Body = body,
            UserId = userId
        });

        Console.WriteLine($"✔ Oprettet post med Id={created.Id}");
    }
}