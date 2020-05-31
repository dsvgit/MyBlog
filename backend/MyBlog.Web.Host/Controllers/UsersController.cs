using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.ApiModels.User;
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
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(BlogDbContext context, UserManager<User> userManager)
        {
            db = context;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("getList")]
        public async Task<ActionResult<PagedResultDto<User>>> GetList(
            [FromQuery] GetAllViewModel getAllViewModel)
        {
            var total = db.Users.Count();
            var users = await db.Users.AsQueryable()
                .Skip(getAllViewModel.Skip)
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
        public async Task<ActionResult<ShowUserModel>> GetOne([FromQuery] GetOneViewModel getOneViewModel)
        {
            var user = await _userManager.FindByIdAsync(getOneViewModel.Id);

            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return new ShowUserModel
            {
                Id = user.Id,
                Email = user.Email,
                RoleIds = userRoles
            };
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
        public async Task<ActionResult<PagedResultDto<User>>> GetManyReference(
            [FromQuery] GetAllViewModel getAllViewModel)
        {
            return await GetList(getAllViewModel); // just mock
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> Create([FromBody] CreateUserModel createUserModel)
        {
            var user = new User {Email = createUserModel.Email, UserName = createUserModel.Email};
            var result = await _userManager.CreateAsync(user, createUserModel.Password);

            if (createUserModel.RoleIds != null && createUserModel.RoleIds.Any())
            {
                await _userManager.AddToRolesAsync(user, createUserModel.RoleIds);
            }

            if (result.Succeeded)
            {
                return CreatedAtAction(nameof(GetOne), new {id = user.Id}, user);
            }

            return BadRequest();
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateUserModel updateUserModel)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (updateUserModel.RoleIds != null && updateUserModel.RoleIds.Any())
            {
                var roles = updateUserModel.RoleIds;
                
                var userRoles = await _userManager.GetRolesAsync(user);
                
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                
                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
            }
            
            var updatedUser = await _userManager.FindByIdAsync(id);

            return Ok(updatedUser);
        }
    }
}