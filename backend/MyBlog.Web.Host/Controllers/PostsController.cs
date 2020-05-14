using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.Models;
using MyBlog.EntityFrameworkCore;

namespace MyBlog.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly BlogDbContext db;
        public PostsController(BlogDbContext context)
        {
            db = context;
        }
        
        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Post>>> Get()
        {
            var users = await db.Posts.ToListAsync();
            return users;
        }
    }
}