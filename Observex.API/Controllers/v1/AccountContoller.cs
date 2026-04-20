using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Observex.Application.DTOs.Identity;
using Observex.Core.Identity;

namespace Observex.API.Controllers.v1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountContoller : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountContoller(UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUser>> RegisterUser(RegisterDto registerDto)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string errorMessages = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem($"Failed to register user: {errorMessages}");
            }

            // Create User 
            ApplicationUser user = new ApplicationUser()
            {
                DisplayUserName = registerDto.UserName,
                UserName = registerDto.UserName,
                FullName = registerDto.FullName,
                Gender = registerDto.Gender,
                Email = registerDto.UserName
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "worker");
                await _signInManager.SignInAsync(user, isPersistent: false);
            }
            else
            {
                string errorMessages = String.Join(", ", result.Errors.Select(e => e.Description));
                return Problem($"Failed to register user: {errorMessages}");
            }

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> IsUserAlreadyRegisterd(string email)
        {
            ApplicationUser? user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return Ok(true);
            }
            return Ok(false);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            // Validation
            if (!ModelState.IsValid)
            {
                string errorMessages = string.Join(", ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return Problem($"Failed to register user: {errorMessages}");
            }

            var result = await _signInManager.PasswordSignInAsync(loginDto.Username, loginDto.Password,
                isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                ApplicationUser? user = await _userManager.FindByEmailAsync(loginDto.Username);

                if (user is null)
                {
                    return NoContent();
                }

                return Ok(new { FullName = user.FullName, Username = user.UserName });
            }
            else
            {
                return Problem("Invalid email or password");
            }
        }

        [HttpGet("logout")]
        public async Task<IActionResult> logout()
        {
            await _signInManager.SignOutAsync();

            return NoContent();
        }
    }
}