using GoodShoppingRD.Models.Auth;
using GoodShoppingRD.Models.Dto;
using GoodShoppingRD.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GoodShoppingRD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;

        public AuthController(UserManager<User> userManager, IAuthService authService)
        {
            _userManager = userManager;
            _authService = authService;
        }

        [Route("LogIn")]
        [HttpGet]
        public async Task<IActionResult> LogInAsync(string userName, string password)
        {
            User user = await ValidateIfUserExistsAsync(userName);

            if (user == null) return BadRequest("Username or passsword Incorrect");

            var response = await _userManager.CheckPasswordAsync(user, password);

            if (response)
            {
                return Ok(_authService.GenerateJwt(user));
            }

            return BadRequest("UserName or Password is incorrect");
        }


        [Route("SignIn")]
        [HttpPost]
        public async Task<IActionResult> SignInAsync(UserRequestDto user)
        {
            try
            {
                var userToBeCreated = new User()
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    PhoneNumber = user.PhoneNumber
                };

                if (_userManager.Users.Any(user => user.PhoneNumber == userToBeCreated.PhoneNumber))
                {
                    return BadRequest("This phone number has been register, try another one");
                }

                var response = await _userManager.CreateAsync(userToBeCreated);

                if (response.Succeeded)
                {
                    var userCreated = await _userManager.FindByNameAsync(user.UserName);
                    var responsePassword = await _userManager.AddPasswordAsync(userCreated, user.Password);

                    if (responsePassword.Succeeded)
                    {
                        return Ok();
                    }
                    return Problem(responsePassword.Errors.First().Description.ToString(), null, 500);
                }
                return Problem(response.Errors.First().Description.ToString(), null, 500);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message, null, 500);
            }

        }    

        [Route("ForgetPassword/{userName}")]
        [HttpGet]
        public async Task<IActionResult> ForgetPassword(string userName)
        {
            User user = await ValidateIfUserExistsAsync(userName);
            
            if (user == null) return BadRequest("Username does not exits");

            string token = await _userManager.GeneratePasswordResetTokenAsync(user);

            return Ok(token);
        }

        [Route("ForgetPassword")]
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(string userName, string token, string newPassword)
        {
            User user = await ValidateIfUserExistsAsync(userName);

            if (user == null) return BadRequest("Username does not exits");

            var response = await _userManager.ResetPasswordAsync(user, token, newPassword);

            if (response.Succeeded)
            {
                return Ok("Password change successfully");
            }

            return Problem(response.Errors.FirstOrDefault().Description, null, 500);
        }

        private async Task<User> ValidateIfUserExistsAsync(string userName)
        {
            var user = new User();

            user = await _userManager.FindByNameAsync(userName);

            if (user == null)
            {
                user = _userManager.Users.Where(user => user.PhoneNumber == userName).FirstOrDefault();                
            }

            return user;
        }
    }
}
