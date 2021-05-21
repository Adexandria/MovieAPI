using Octokit;
using AutoMapper;
using MoviesAPI.Model;
using MoviesAPI.UserModel;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    [AllowAnonymous]
    public class UsersController : ControllerBase
    {
        readonly UserManager<Users> user;
        private readonly SignInManager<Users> login;
        private readonly IPasswordHasher<Users> passwordHasher;

        readonly IMapper mapper;

        public UsersController(UserManager<Users> user, IMapper mapper, SignInManager<Users> login, IPasswordHasher<Users> passwordHasher)
        {
            this.user = user;
            this.mapper = mapper;
            this.login = login;
            this.passwordHasher = passwordHasher;
        }

        [HttpPost("signup")]
        public async Task<ActionResult> SignUp(SignUpModel newuser)
        {
            var signup = mapper.Map<Users>(newuser);
            if (newuser.Password.Equals(newuser.RetypePassword))
            {
                IdentityResult identity = await user.CreateAsync(signup, signup.PasswordHash);

                if (identity.Succeeded)
                {
                    await user.AddClaimAsync(signup, new Claim(ClaimTypes.Role, "User"));
                    await login.SignInAsync(signup, false);
                    return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{signup.UserName} Check out the movies on rental");
                }
                else
                {
                    return BadRequest("The username exists or check password requirements");
                }
            }
            return this.StatusCode(StatusCodes.Status400BadRequest, "Password not equal,retype password");
        }
    
        [HttpPost("socialmedia/signup")]
        public async Task<ActionResult> SocialMediaSignUp(GithubSignUpModel signUpModel)
        {
            var isSuccess = await GetUser(signUpModel.UserName);
            if (isSuccess == "Ok")
            {
                var newUser = mapper.Map<SignUpModel>(signUpModel);
                return await SignUp(newUser);
            }
            else 
            {
                return BadRequest("No user found");
            }
        }
           
        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var logindetails = mapper.Map<Users>(model);
            var currentUser = await user.FindByNameAsync(logindetails.UserName);
            if (currentUser == null) return NotFound("Username doesn't exist");
            var passwordVerifyResult = passwordHasher.VerifyHashedPassword(currentUser, currentUser.PasswordHash, model.Password);
            if (passwordVerifyResult.ToString() == "Success")
            {
                await login.SignInAsync(currentUser, false);
                await login.CreateUserPrincipalAsync(currentUser);

                return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{currentUser.UserName} Check out the movies on rental");
            }

            return BadRequest("password is not correct");
        }

        [HttpPost("socialmedia/login")]
        public async Task<ActionResult> SocialMediaLogin(GithubLoginModel githubLogin) 
        {
            var isSuccess = await GetUser(githubLogin.UserName);
            if (isSuccess == "Ok")
            {
                var currentUser = mapper.Map<LoginModel>(githubLogin);
                return await Login(currentUser);
            }
            else
            {
                return BadRequest("No user found");
            }
        }

        [HttpGet("{username}")]
        public async Task<ActionResult> ResetPassword(string username) 
        {
            var currentuser = await user.FindByNameAsync(username);
            if (currentuser == null) return NotFound("username doesn't exist");
            var passwordResetToken = await user.GeneratePasswordResetTokenAsync(currentuser);
            return Ok($"Reset Password Token {passwordResetToken}");
        }

        [HttpPost("resetpassword/{username}")]
        public async Task<ActionResult> VerifyToken(ResetPassword resetPassword,string username) 
        {
            var currentuser = await user.FindByNameAsync(username);
            if (currentuser == null) return NotFound("username doesn't exist");
            var isVerifyResult = await user.ResetPasswordAsync(currentuser, resetPassword.Token, resetPassword.NewPassword);
            if (isVerifyResult.Succeeded)
            {
                return Ok("Password changed");
            }
            else
            {
                return BadRequest(isVerifyResult.Errors);
            }
        }

        [HttpPost("{username}/signout")]
        public async Task<ActionResult> Signout(string username)
        {
            var currentuser = await user.FindByNameAsync(username);
            if (currentuser == null)
            {
                return NotFound();
            }
            await login.SignOutAsync();
            return Ok();
        }

        private async Task<string> GetUser(string username)
        {
            var github = new GitHubClient(new ProductHeaderValue("MovieAPI"));
            var user = await github.User.Get(username);
            if (user != null)
            {
                return "Ok";
            }
            return "Not Found";
        }
    }
}
