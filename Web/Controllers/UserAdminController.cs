using Application.DTOs;
using Application.Interfaces;
using Application.Models;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = nameof(UserRole.Admin))]

    public class UserAdminController : ControllerBase
    {
        private readonly IUserAdminService _userAdminService;

        public UserAdminController(IUserAdminService userAdminService)
        {
            _userAdminService = userAdminService;
        }
        [HttpGet("Users")]
        public IActionResult GetUsers()
        {
            var users = _userAdminService.GetUsers();
            return Ok(users);
        }
        [HttpGet("Albums")]
        public IActionResult GetAlbums()
        {
            var albums = _userAdminService.GetAlbums();
            return Ok(albums);
        }
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

        [HttpPut("Update-Role/{id}")]
        public IActionResult UpdateRole([FromRoute]int id, [FromBody]UserRoleUpdateDTO userRoleUpdateDTO)
        {

            var user = _userAdminService.UpdateRole(id, userRoleUpdateDTO);
            return Ok(user);
        }
        [HttpPut("Update-Album-State/{id}")]
        public IActionResult UpdateStateAlbum(int id, [FromBody] UpdateAlbumStateDto updateAlbumStateDto)
        {
            _userAdminService.UpdateAlbumState(id, updateAlbumStateDto);
            return Ok($"Estado del album actualizado a {(updateAlbumStateDto.State ? "Aceptado" : "Rechazado")}");
        }
       
        [HttpDelete("/Delete-User/{id}")]
        public IActionResult DeleteUser([FromRoute]int id)
        {
           var user = _userAdminService.DeleteUser(id);
            return Ok("Usuario eliminado");
        }
    }
}
