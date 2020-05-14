using System.Linq;
using MyBlog.Core.Models;
using MyBlog.EntityFrameworkCore;

namespace MyBlog.Web.Mvc
{
    public static class SampleData
    {
        public static void Initialize(BlogDbContext context)
        {
            if (!context.Posts.Any())
            {
                context.Posts.AddRange(
                    new Post
                    {
                        Title = "First post",
                        Text = "Hi there! this is my first post"
                    },
                    new Post
                    {
                        Title = "Evening",
                        Text = "Today I started to creating this blog!"
                    },
                    new Post
                    {
                        Title = "Breakfast",
                        Text = "My breakfast was nice!"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}