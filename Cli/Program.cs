// Server/Cli/Program.cs
using InMemoryRepositories;
using RepositoryContracts;

IPostRepository postRepo = new PostInMemoryRepository();
IUserRepository userRepo = new UserInMemoryRepository();
ICommentRepository commentRepo = new CommentInMemoryRepository();

var app = new Cli.CliApp(userRepo, postRepo, commentRepo);
await app.RunAsync();