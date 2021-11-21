using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClassLibrary.Models;
using ReunitePetsWebAPI.Services;
using AutoMapper;
using ReunitePetsWebAPI.DTOs;

namespace ReunitePetsWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppUsersController : ControllerBase
    {
        private IUserRepository _userRepository;
        private readonly IMapper _mapper; 

        public AppUsersController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // POST: api/AppUsers/Authenticate
        [HttpPost]
        public async Task<ActionResult<AppUser>> CreateUser([FromBody] UserCredentialDto user)
        {
            bool isUserExist = await _userRepository.UserExists(user.Username);

            if (isUserExist)
            {
                return StatusCode(500, "Username already exist.");
            }
            else
            {
                AppUser userToInsert = _mapper.Map<AppUser>(user);
                await _userRepository.CreateUser(userToInsert);
                if (!await _userRepository.Save())
                {
                    return StatusCode(500, "A problem occured while processing the request");
                }
                else
                {
                    return StatusCode(200, "User created.");
                }
            }
        }

        // POST: api/AppUsers/Authenticate
        [HttpPost("Authenticate")]
        public async Task<ActionResult<AppUser>> AuthenticateUser([FromBody] UserCredentialDto appUser)
        {
            AppUser userToAuthenticate = _mapper.Map<AppUser>(appUser);
            bool result = await _userRepository.AuthenticateUser(userToAuthenticate);

            if (result)
            {
                var authenticatedUser = _mapper.Map<UserDto>(userToAuthenticate);
                return Ok(authenticatedUser);
            }
            else
            {
                return StatusCode(404, "Username or password is incorrect.");
            }
        }
    }
}
