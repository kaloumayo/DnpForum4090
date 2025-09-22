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
        var title = Console.ReadLine() ?? "";
        Console.Write("Brødtekst: ");
        var body = Console.ReadLine() ?? "";
        Console.Write("Forfatter UserId: ");
        var ok = int.TryParse(Console.ReadLine(), out int userId);
        if (!ok) { Console.WriteLine("Ugyldigt UserId."); return; }

        _ = await _users.GetSingleAsync(userId);
        var created = await _posts.AddAsync(new Post { Title = title, Body = body, UserId = userId });
        Console.WriteLine($"✔ Oprettet post Id={created.Id}");
    }
}