using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.Model;
using MoviesAPI.Services;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesAPI.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        readonly IUser user;
        public UsersController(IUser user)
        {
            this.user = user;
        }
         public ActionResult<Users> GetAllUsers()
        {
            return Ok(user.GetUsers);
        }
        [HttpGet("{id}")]
        public ActionResult<Users> GetUser(Guid id) 
        {
            var currentuser = user.GetUserById(id);
            if(currentuser != null) 
            {
               return Ok(currentuser);
            }
            return NotFound();
        }
        [HttpPost]
        public ActionResult<Users> AddUser(Users users) 
        {
             if(users != null) 
            {
                user.Add(users);
                user.Save();
                return Ok(users);
            }
            return NotFound();
        }
        [HttpPut("{id}")]
        public ActionResult<Users> UpdateUser(Users users,Guid id) 
        {
            var currentuser = user.GetUserById(id);
            if(currentuser != null) 
            {
               var query =  user.Update(users);
               user.Save();
               return query;
            }
            return NotFound();
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteUser (Guid id) 
        {
            user.Delete(id);
            user.Save();
            return NoContent();
        }
    }
}
