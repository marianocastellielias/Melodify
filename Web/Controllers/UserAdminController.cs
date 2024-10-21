using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        
        [HttpGet]
        public IActionResult GetUsers()
        {
            var users = _userAdminService.GetUsers();
            return Ok(users);
        }
    }
}
