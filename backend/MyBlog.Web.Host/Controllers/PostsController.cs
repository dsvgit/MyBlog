using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.ApiModels.Post;
using MyBlog.Core.DtoModels;
using MyBlog.Core.Models;
using MyBlog.Core.ViewModels;
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
        
        [HttpGet]
        [Route("getList")]
        public async Task<ActionResult<PagedResultDto<Post>>> GetList(
            [FromQuery] GetAllViewModel getAllViewModel)
        {
            var total = db.Posts.Count();
            var posts = await db.Posts.AsQueryable()
                .Skip(getAllViewModel.Skip)
                .Take(getAllViewModel.Size)
                .ToListAsync();

            return new PagedResultDto<Post>
            {
                TotalCount = total,
                Items = posts
            };
        }

        [HttpGet]
        [Route("getOne")]
        public async Task<ActionResult<ShowPostModel>> GetOne([FromQuery] int id)
        {
            var post = await db.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return new ShowPostModel
            {
                Id = post.Id,
                Title = post.Title,
                Text = post.Text
            };
        }
    }
}