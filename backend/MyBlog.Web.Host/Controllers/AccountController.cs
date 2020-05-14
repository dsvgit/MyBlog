using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MyBlog.Core.Models;
using MyBlog.Web.Host.ViewModels;

namespace MyBlog.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager { get; }
        private SignInManager<User> _signInManager { get; }

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        [HttpPost("token")]
        public async Task<IActionResult> Token([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(loginModel.Email);
                var roles = await _userManager.GetRolesAsync(user);
                var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginModel.Password, false);
                if (signInResult.Succeeded)
                {
                    var now = DateTime.UtcNow;
                    var key = AuthOptions.GetSymmetricSecurityKey();
                    var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var claims = new[]
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email)
                    };

                    var token = new JwtSecurityToken(
                        issuer: AuthOptions.ISSUER,
                        audience: AuthOptions.AUDIENCE,
                        notBefore: now,
                        claims: claims,
                        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                        signingCredentials: signingCredentials
                    );

                    var response = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo,
                        email = user.Email
                    };

                    return Json(response);
                }
                else
                {
                    return BadRequest(new {message = "Invalid username or password."});
                }
            }

            return BadRequest();
        }
    }
}