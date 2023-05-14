using CapstoneProjectLibrary.Interfaces;
using CapstoneProjectLibrary.Models;
using CapstoneProjectLibrary.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using CapstoneProjectLibrary;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace CapstoneProjectAPI.Controllers
{
    public class AccountController : Controller
    {
        public IRepository GameRepo { get; set; }
        public IGenresRepository GenresRepo { get; set; }
        public IUsersRepository UsersRepo { get; set; }

        private EntityContext _context;
        public AccountController(IRepository gameRepo, IGenresRepository genresRepo, IUsersRepository usersRepo)
        {
            GameRepo = gameRepo;
            GenresRepo = genresRepo;
            UsersRepo = usersRepo;
            _context = ((UsersRepository)UsersRepo).entityContext;
        }

        [HttpPost]
        [Route("api/auth/register")]       
        public async Task<IActionResult> Register(string firstName, string lastName, string userName, string email, string password)
        {                
                User user = await _context.users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                UsersRepo.AddUser(
                    firstName,
                    lastName,
                     userName,
                     email,
                     password
                );

                await Authenticate(email);
                return Ok("/");
            }
            else
                return Problem("User already exist");
        }

        [HttpGet]
        [Route("api/auth/authCheck")]
        [Authorize]
        public async Task<IActionResult> CheckAuthorization()
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/auth/signOut")]
        [Authorize]
        public async Task<IActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }

        [HttpGet]
        [Route("api/auth/getUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {
            var UserClaim = HttpContext.User;
            var email = UserClaim.FindFirst(ClaimsIdentity.DefaultNameClaimType).Value;
            var user = _context.users.Where(u => u.Email == email).Select(u => new {Email = u.Email,
                FirstName = u.FirstName, LastName = u.LastName, AvatarUrl = u.AvatarUrl}).FirstOrDefault();
            return Ok(user);
        }

        [HttpPost]
        [Route("api/auth/login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            
                User user = await _context.users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
                if (user != null)
                {
                    await Authenticate(email);

                    return Ok("/");
                }
            return Problem("Bad login or password");
        }

        private async Task Authenticate(string userName)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
