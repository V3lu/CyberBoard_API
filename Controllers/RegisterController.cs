using CyberBoardAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CyberBoardAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly CyberBoardDBContext _dbContext;

        public RegisterController(CyberBoardDBContext context)
        {
            _dbContext = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAgent([FromBody] Agent agent)
        {

        }

        [HttpPost]
        public async Task<IActionResult> RegisterAgency([FromBody] Agency agency)
        {

        }
    }
}
