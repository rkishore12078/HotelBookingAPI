using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using UserAPI.Exceptions;
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
            try
            {
                UserDTO user = _userService.Register(userRegisterDTO);
                if (user == null)
                    return BadRequest(new Error(2, "Registration Not Successfull"));
                return Created("User Registered", user);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4,ex.Message));
            }
        }

        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<UserDTO> LogIN(UserDTO userDTO)
        {
            try
            {
                UserDTO user = _userService.LogIN(userDTO);
                if (user == null)
                    return BadRequest(new Error(1, "Invalid UserName or Password"));
                return Ok(user);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }

        [ProducesResponseType(typeof(UserDTO), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<UserDTO> Update(UserRegisterDTO user)
        {
            try
            {
                var myUser = _userService.Update(user);
                if (myUser == null)
                    return NotFound(new Error(3, "Unable to Update"));
                return Created("User Updated Successfully", myUser);
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }

        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]//Success Response
        [ProducesResponseType(StatusCodes.Status404NotFound)]//Failure Response
        [HttpPost]
        public ActionResult<string> Update_Password(UserDTO user)
        {
            try
            {
                bool myUser = _userService.Update_Password(user);
                if (myUser)
                    return NotFound(new Error(3, "Unable to Update Password"));
                return Ok("Password Updated Successfully");
            }
            catch (InvalidSqlException ise)
            {
                return BadRequest(new Error(3, ise.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(new Error(4, ex.Message));
            }
        }
    }
}
