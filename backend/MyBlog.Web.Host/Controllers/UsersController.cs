using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.DtoModels;
using MyBlog.Core.Models;
using MyBlog.Core.ViewModels;
using MyBlog.EntityFrameworkCore;

namespace MyBlog.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly BlogDbContext db;
        private readonly UserManager<User> _userManager;

        public UsersController(BlogDbContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("getList")]
        public async Task<ActionResult<PagedResultDto<User>>> GetList([FromQuery] GetAllViewModel getAllViewModel)
        {
            var total = db.Users.Count();
            var users = await db.Users.AsQueryable()
                .Skip(getAllViewModel.Page)
                .Take(getAllViewModel.Size)
                .ToListAsync();

            return new PagedResultDto<User>
            {
                TotalCount = total,
                Items = users
            };
        }

        [HttpGet]
        [Route("getOne")]
        public async Task<ActionResult<User>> GetOne([FromQuery] GetOneViewModel getOneViewModel)
        {
            var user = await db.Users.FindAsync(getOneViewModel.Id);

            return user;
        }

        [HttpGet]
        [Route("getMany")]
        public async Task<ActionResult<IEnumerable<User>>> GetMany([FromQuery] GetManyViewModel getManyViewModel)
        {
            var users = await db.Users.AsQueryable()
                .Where(user => getManyViewModel.Ids.Contains(user.Id))
                .ToListAsync();

            return users;
        }

        [HttpGet]
        [Route("getManyReference")]
        public async Task<ActionResult<PagedResultDto<User>>> GetManyReference([FromQuery] GetAllViewModel getAllViewModel)
        {
            return await GetList(getAllViewModel); // just mock
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create(UserDto userDto)
        {
            var user = new User {Email = userDto.Email, UserName = userDto.Email};
            var result = await _userManager.CreateAsync(user, userDto.Password);

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetOne), new {id = user.Id}, user);
            }
           
            return BadRequest();
        }
    }
}