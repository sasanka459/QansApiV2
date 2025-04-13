using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QansBAL.Abstraction;
using QansBAL.Models;
using QansDAL.Entities;

namespace QansApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }



        [HttpGet(Name = "GetUser")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUser()
        {
            var Users =await _userService.GetUser();
            return Users == null|| Users?.Count()==0 ? NotFound() : Ok(Users);
        }

    }
}
