using System;
using System.Net.Mail;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Model;
using MoviesAPI.UserModel;
using Newtonsoft.Json;
using Reddit_NewsLetter.ViewDTO;

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
            var emailExists = await user.FindByEmailAsync(newuser.Email);
            if (emailExists != null) return BadRequest("Eamil already been used");
            if (newuser.Password.Equals(newuser.RetypePassword))
            {
                IdentityResult identity = await user.CreateAsync(signup,signup.PasswordHash);

                if (identity.Succeeded)
                {   
                    await user.AddClaimAsync(signup, new Claim(ClaimTypes.Role, "User"));
                    var emailtoken = await user.GenerateEmailConfirmationTokenAsync(signup);
                    var emailLink = CreateEmailVerificationLink(emailtoken,signup.UserName);
                    var json = JsonConvert.SerializeObject(emailLink);
                    //SendMessage(json, signup.Email);
                    return CreatedAtRoute("EmailVerification", new { username = signup.UserName, token = emailtoken }, $"Welcome {signup.UserName} Account has't been verified ");

                }
                else
                {
                    return BadRequest("The username exists or check password requirements");
                }
            }
            return this.StatusCode(StatusCodes.Status400BadRequest, $"Password not equal,retype password");

           
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginModel model)
        {
            var logindetails = mapper.Map<Users>(model);
            var currentuser = await user.FindByNameAsync(logindetails.UserName);
            if (currentuser == null) return NotFound("Username doesn't exist");
            var passwordVerifyResult = passwordHasher.VerifyHashedPassword(currentuser, currentuser.PasswordHash, model.Password);
            if (passwordVerifyResult.ToString() == "Success" && currentuser.EmailConfirmed == true)
            {
                await login.SignInAsync(currentuser, false);
                await login.CreateUserPrincipalAsync(currentuser);

                return this.StatusCode(StatusCodes.Status200OK, $"Welcome,{currentuser.UserName} Check out the movies on rental");
            }
         
            return BadRequest("password is not correct");
        }

        [HttpPost("{username}/{token}",Name = "EmailVerification")]
        public async Task<ActionResult> EmailVerify(string token,string username) 
        {
            var currentuser = await user.FindByNameAsync(username);
            if(currentuser == null) return NotFound("username doesn't exist");
            var charToRemove = new string[] {"%2","%3"};
            foreach (var c in charToRemove)
            {
                token = token.Replace(c, string.Empty);
            }
            
            
            var isVerifyResult = await user.ConfirmEmailAsync(currentuser, token);
            if (isVerifyResult.Succeeded) 
            {
                return Ok("Account is verified");
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
        private void SendMessage(string link,string email) 
        {
            MailMessage message = new MailMessage();
            SmtpClient smtp = new SmtpClient();
            message.From = new MailAddress("adeolaaderibigbe09@gmail.com");
            message.To.Add(email);
            message.Subject = "Email confirmation from Movies API";
            message.Body = $"click on the link to confirm your account {link}";
            smtp.Send(message);
            

        }
        public LinkDto CreateEmailVerificationLink(string token,string username)
        {
            var links = new LinkDto(Url.Link("EmailVerification", new { username,token }),
           "email_Verification",
           "POST");
            return links;

        }
    }
}
