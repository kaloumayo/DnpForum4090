using System.Linq;
using Entities;
using RepositoryContracts;

namespace Cli.UI.ManagePosts;

public class SinglePostView
{
    private readonly IPostRepository _posts;
    private readonly IUserRepository _users;
    private readonly ICommentRepository _comments;

    public SinglePostView(IPostRepository posts, IUserRepository users, ICommentRepository comments)
    {
        _posts = posts;
        _users = users;
        _comments = comments;
    }

    public async Task RunAsync()
    {
        Console.Write("Angiv PostId: ");
        var ok = int.TryParse(Console.ReadLine(), out int id);
        if (!ok) { Console.WriteLine("Ugyldigt Id."); return; }

        var p = await _posts.GetSingleAsync(id);
        Console.WriteLine($"\n#{p.Id} {p.Title}\n{p.Body}\n— UserId={p.UserId}");

        var related = _comments.GetMany().Where(c => c.PostId == p.Id).OrderBy(c => c.Id).ToList();
        Console.WriteLine("\n[Comments]");
        if (related.Count == 0) Console.WriteLine("(ingen)");
        foreach (var c in related) Console.WriteLine($"- ({c.Id}) UserId={c.UserId}: {c.Body}");

        Console.WriteLine("\n1) Add comment   0) Back");
        Console.Write("Valg: ");
        var choice = Console.ReadLine();
        if (choice == "1")
        {
            await AddCommentAsync(p.Id);
        }
    }

    private async Task AddCommentAsync(int postId)
    {
        Console.Write("UserId: ");
        var okU = int.TryParse(Console.ReadLine(), out int userId);
        if (!okU) { Console.WriteLine("Ugyldigt UserId."); return; }

        Console.Write("Kommentar: ");
        var body = Console.ReadLine() ?? "";

        _ = await _users.GetSingleAsync(userId);
        _ = await _posts.GetSingleAsync(postId);

        var created = await _comments.AddAsync(new Comment { PostId = postId, UserId = userId, Body = body });
        Console.WriteLine($"✔ Kommentar oprettet Id={created.Id}");
    }
}