using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using MyBlog.Core.Models;
using MyBlog.Web.Mvc.ViewModels;

namespace MyBlog.Web.Mvc.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountApiController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountApiController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, false);
                if (result.Succeeded)
                {
                    return Ok("You logged");
                }

                return Ok("Wrong email or password");
            }

            return Ok("Model is invalid");
        }
    }
}