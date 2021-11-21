using ApiForum.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using ApiForum.DataBase.Models;
using Microsoft.AspNetCore.Identity;

namespace ApiForum.Controllers
{
    [ApiController]
    [Route("/user/[action]")]
    public class AccountController : Controller
    {
        private UserManager<User> _userManager { get; set; }
      
        [HttpPost]
        public async Task<IActionResult> RegistrationUser(string email, string password)
        {
            if (email != null && password != null)
            {
                User newUser = new User() { Email = email, UserName=email };
                var signedUser = await _userManager.CreateAsync(newUser, password);
                if (signedUser.Succeeded)
                {
                    return await GetToken(email,password);
                }
                return BadRequest();
            }
            return BadRequest();
        }

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;

        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost] 
        public async Task<IActionResult> GetToken(string login, string password)
        {

            var identity = await GetIdentityAsync(login, password);
            if (identity == null)
            {
                return BadRequest(new { error="User isn`t found"});
            }

            var now = DateTime.Now;
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
               
                claims: identity.Claims,
                expires:DateTime.Now.AddMinutes(AuthOptions.LIFETIME) ,
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetSecurityKey(), SecurityAlgorithms.HmacSha256));
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
                var resp = new { access_token = encodedJwt };


            return Json(resp);
        }

        private async Task<ClaimsIdentity> GetIdentityAsync(string login, string password)
        {
            User User = await _userManager.FindByEmailAsync(login);
            if (User != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, User.UserName),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType,password)
                };
                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token",ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null; 
        }
    }
}
