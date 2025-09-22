using System.Linq;
using RepositoryContracts;

namespace Cli.UI.ManagePosts;

public class ListPostsView
{
    private readonly IPostRepository _posts;
    public ListPostsView(IPostRepository posts) => _posts = posts;

    public Task RunAsync()
    {
        Console.WriteLine("\n[Posts]");
        var list = _posts.GetMany().OrderBy(p => p.Id).ToList();
        if (list.Count == 0) Console.WriteLine("(ingen)");
        foreach (var p in list) Console.WriteLine($"({p.Id}) {p.Title}");
        return Task.CompletedTask;
    }
}

