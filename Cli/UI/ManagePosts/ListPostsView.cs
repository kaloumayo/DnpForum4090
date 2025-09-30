using System;
using System.Linq;
using System.Threading.Tasks;
using RepositoryContracts;

namespace Cli.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _posts;
    private readonly IUserRepository _users;

    public ListPostsView(IPostRepository posts, IUserRepository users)
    {
        _posts = posts;
        _users = users;
    }

    public async Task RunAsync()
    {
        var all = _posts.GetMany().ToList();
        if (!all.Any())
        {
            Console.WriteLine("Ingen posts fundet.");
            return;
        }

        Console.WriteLine("Posts:");
        foreach (var p in all.OrderBy(p => p.Id))
        {
            string author = "?";
            try { author = (await _users.GetSingleAsync(p.UserId)).UserName; }
            catch { /* hvis user ikke findes */ }

            Console.WriteLine($"- [{p.Id}] {p.Title} (af {author})");
        }
    }
}