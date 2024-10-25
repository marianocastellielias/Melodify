using Application.DTOs;
using Application.Interfaces;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class UserAdminController : ControllerBase
    {
        private readonly IUserAdminService _userAdminService;

        public UserAdminController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            var users = _userAdminService.GetUsers();
            return Ok(users);
        }
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("Update-Users/{id}")]
        public IActionResult PutUser([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            var user = _userAdminService.UsersUpdate(id, updateUserDto);
            return Ok(user);
        }

        [HttpPost("Create-User")]
        public IActionResult CreateUser([FromBody] AddUserDto addUserDto)
        {
            var user = _userAdminService.AddUser(addUserDto);
            return Ok(user);
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("Update-Role/{id}")]
        public IActionResult UpdateRole([FromRoute]int id, [FromBody]UserRoleUpdateDTO userRoleUpdateDTO)
        {

            var user = _userAdminService.UpdateRole(id, userRoleUpdateDTO);
            return Ok(user);
        }

        [Authorize(Roles = $"{nameof(UserRole.Artist)},{nameof(UserRole.Client)}") ]
        [HttpPut("Update-User-Data")]
        public IActionResult UpdateUserData( UpdateUserDto updateUser)
        {
            var userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value ?? "");

            _userAdminService.UserUpdate(userId, updateUser);

            return Ok();
               
        }
        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpDelete("/Delete-User/{id}")]
        public IActionResult DeleteUser([FromRoute]int id)
        {
           var user = _userAdminService.DeleteUser(id);
            return Ok("Usuario eliminado");
        }
    }
}
