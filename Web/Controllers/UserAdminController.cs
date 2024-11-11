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
        [HttpGet("Albums")]
        public IActionResult GetAlbums()
        {
            var albums = _userAdminService.GetAlbums();
            return Ok(albums);
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
            try
            {
                var user = _userAdminService.AddUser(addUserDto);
                return Ok(user); // Si el usuario se crea correctamente, retorna un código 200 OK
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // En caso de error, retorna un código 400 Bad Request
            }
        }


        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("Update-Role/{id}")]
        public IActionResult UpdateRole([FromRoute] int id, [FromBody] UserRoleUpdateDTO userRoleUpdateDTO)
        {
            try
            {
                var user = _userAdminService.UpdateRole(id, userRoleUpdateDTO);
                return Ok(new { message = "Rol actualizado con éxito" }); // Respuesta con mensaje de éxito
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message }); // Respuesta con mensaje de error
            }
        }

        [Authorize(Roles = nameof(UserRole.Admin))]
        [HttpPut("Update-Album-State/{id}")]
        public IActionResult UpdateStateAlbum(int id, [FromBody] UpdateAlbumStateDto updateAlbumStateDto)
        {
            _userAdminService.UpdateAlbumState(id, updateAlbumStateDto);
            return Ok($"Estado del album actualizado a {(updateAlbumStateDto.State ? "Aceptado" : "Rechazado")}");
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
