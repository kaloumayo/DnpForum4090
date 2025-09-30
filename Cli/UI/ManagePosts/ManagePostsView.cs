using RepositoryContracts;

namespace Cli.UI.ManagePosts;

public class ManagePostsView
{
    private readonly IPostRepository _posts;
    private readonly IUserRepository _users;
    private readonly ICommentRepository _comments;

    public ManagePostsView(IPostRepository posts, IUserRepository users, ICommentRepository comments)
    {
        _posts = posts;
        _users = users;
        _comments = comments;
    }

    public async Task RunAsync()
    {
        while (true)
        {
            Console.WriteLine("\n=== Manage Posts ===");
            Console.WriteLine("1) Create post");
            Console.WriteLine("2) List posts");
            Console.WriteLine("3) Single post");
            Console.WriteLine("0) Back");
            Console.Write("Valg: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await new CreatePostView(_posts, _users).RunAsync();
                    break;
                case "2":
                    await new ListPostsView(_posts, _users).RunAsync();
                    break;
                case "3":
                    await new SinglePostView(_posts, _users, _comments).RunAsync();
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