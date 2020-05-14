using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Models;

namespace MyBlog.EntityFrameworkCore
{
    public class BlogDbContext : IdentityDbContext<User>
    {
        public DbSet<Post> Posts { get; set; }
        //public DbSet<Topic> Topics { get; set; }

        public BlogDbContext(DbContextOptions<BlogDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
