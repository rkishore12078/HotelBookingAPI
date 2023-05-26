using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Interfaces;
using UserAPI.Models;
using UserAPI.Models.DTO;
using UserAPI.Services;

namespace UserAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<UserDTO> Register(UserRegisterDTO userRegisterDTO)
        {
            UserDTO user = _userService.Register(userRegisterDTO);
            if (user == null)
                return BadRequest(new Error(2, "Register Not Successfull"));
            return Created("User Registered", user);
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<UserDTO> LogIN(UserDTO userDTO)
        {
            UserDTO user = _userService.LogIN(userDTO);
            if (user == null)
                return BadRequest(new Error(1, "Invalid UserName or Password"));
            return Ok(user);
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<UserDTO> Update(User user)
        {
            var myUser=_userService.Update(user);
            if (myUser == null)
                return NotFound(new Error(3,"Unable to Update"));
            return Ok(new Error(4,"Updated Successfully"));
        }
    }
}
