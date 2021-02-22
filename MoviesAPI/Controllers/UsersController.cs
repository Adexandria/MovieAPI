﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MoviesAPI.DTO;
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
        readonly IMapper mapper;
        public UsersController(IUser user,IMapper mapper)
        {
            this.user = user;
            this.mapper = mapper;
        }
        //Get all Users
         public ActionResult<UserDTO> GetAllUsers()
         {
            var users = user.GetUsers;
            var newusers = mapper.Map<IEnumerable<UserDTO>>(users);
            return Ok(newusers);
         }
        //Get an Individual
        [HttpGet("{id}",Name ="user")]
        public async Task<ActionResult<UserDTO>> GetUser(Guid id) 
        {
            var currentuser = await user.GetUserById(id);
            if(currentuser != null) 
            {
                var user = mapper.Map<UserDTO>(currentuser);
                return Ok(user);
            }
            return NotFound();
        }
        //To Add user
        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser(UserCreateDTO newuser) 
        {
             if(newuser != null) 
            {
                var currentuser = mapper.Map<Users>(newuser);
                await user.Add(currentuser);
                 await user.Save();
                var addeduser = mapper.Map<UserDTO>(currentuser);
                return Ok(addeduser);
            }
            return NotFound();
        }
        //To edit or add chnages to an existing user
        [HttpPut("{id}")]
        public async Task<ActionResult<UserDTO>> UpdateUser(Users users,Guid id) 
        {
            var currentuser =await user.GetUserById(id);
            if(currentuser != null) 
            {
              var query =  user.Update(users);
              await user.Save();
              var addeduser = mapper.Map<UserDTO>(currentuser);
              return Ok(addeduser);
            }
            return NotFound();
        }
        //To delete auser from the database
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser (Guid id) 
        {
           await user.Delete(id);
            return NoContent();
        }
    }
}
