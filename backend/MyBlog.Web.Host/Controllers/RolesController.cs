using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBlog.Core.ApiModels.Role;
using MyBlog.Core.DtoModels;
using MyBlog.Core.ViewModels;
using MyBlog.EntityFrameworkCore;
using MyBlog.Web.Host.ViewModels;

namespace MyBlog.Web.Host.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly BlogDbContext db;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(BlogDbContext context, RoleManager<IdentityRole> roleManager)
        {
            db = context;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Route("getList")]
        public async Task<ActionResult<PagedResultDto<ListItemRoleModel>>> GetList(
            [FromQuery] GetAllViewModel getAllViewModel)
        {
            var total = db.Roles.Count();
            var roles = await db.Roles.AsQueryable()
                .Skip(getAllViewModel.Skip)
                .Take(getAllViewModel.Size)
                .Select(role => new ListItemRoleModel
                {
                    Id = role.Name,
                    Name = role.Name
                })
                .ToListAsync();

            return new PagedResultDto<ListItemRoleModel>
            {
                TotalCount = total,
                Items = roles
            };
        }

        [HttpGet]
        [Route("getOne")]
        public async Task<ActionResult<ShowRoleModel>> GetOne([FromQuery] GetOneViewModel getOneViewModel)
        {
            var role = await db.Roles.AsQueryable()
                .FirstAsync(role => role.Name == getOneViewModel.Id);

            return new ShowRoleModel
            {
                Id = role.Name,
                Name = role.Name
            };
        }

        [HttpGet]
        [Route("getMany")]
        public async Task<ActionResult<IEnumerable<ListItemRoleModel>>> GetOne(
            [FromQuery] GetManyViewModel getManyViewModel)
        {
            var roles = await db.Roles.AsQueryable()
                .Where(role => getManyViewModel.Ids.Contains(role.Name))
                .Select(role => new ListItemRoleModel
                {
                    Id = role.Name,
                    Name = role.Name
                })
                .ToListAsync();

            return roles;
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<IdentityRole>> Create([FromBody] CreateRoleModel createRoleViewModel)
        {
            var name = createRoleViewModel.Name;

            if (!string.IsNullOrEmpty(name))
            {
                var role = new IdentityRole(name);
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return CreatedAtAction(nameof(GetOne), new {id = role.Id}, role);
                }
            }

            return BadRequest();
        }
    }
}