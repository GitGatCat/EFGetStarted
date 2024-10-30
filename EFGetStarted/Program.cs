using System;
using System.Linq;

using var db = new BloggingContext();

// Note: This sample requires the database to be created before running.
Console.WriteLine($"Database path: {db.DbPath}.");

// Create
Console.WriteLine("Inserting a new blog");
db.Add(new Blog { Url = "http://blogs.msdn.com/adonet" });
db.SaveChanges();

// Read
Console.WriteLine("Querying for a blog");
var blog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

// Update
Console.WriteLine("Updating the blog and adding a post");
blog.Url = "https://devblogs.microsoft.com/dotnet";
blog.Posts.Add(
    new Post { Title = "Hello World", Content = "I wrote an app using EF Core!" });
db.SaveChanges();

// Delete
Console.WriteLine("Delete the blog");
db.Remove(blog);
db.SaveChanges();

Console.WriteLine("Lager min egen blog");
db.Add(new Blog { Url = "https://blog.petterelvevoll.no/tekblog" });
db.SaveChanges();

Console.WriteLine("Bruker Linq for å finne igjen min egen blog");
var tekBlog = db.Blogs
    .OrderBy(b => b.BlogId)
    .First();

Console.WriteLine("Legger ut de første blog postene");
tekBlog.Posts.Add(
    new Post { Title = "C# med EF", Content = "Første blog laget med Entity Foundation Core!" });
tekBlog.Posts.Add(
    new Post { Title = "C# + EF + Razor + ASP.Net", Content = "Nettsiden er nå live :D"});
db.SaveChanges();

var mine_blog_poster = tekBlog.Posts
    .OrderBy(b => b.PostId);

Console.WriteLine("Alle mine poster levert rett til skjermen\n");
foreach (var post in mine_blog_poster)
{
    Console.WriteLine("\t" + post.Title);
    Console.WriteLine("\t\t" + post.Content);
}

Console.WriteLine("Sletter bloggen min :(");
db.Remove(tekBlog);
db.SaveChanges();