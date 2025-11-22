using CyberBoardAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberBoardAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly CyberBoardDBContext _dbContext;
        private readonly IConfiguration _configuration;

        public LoginController(CyberBoardDBContext context, IConfiguration configuration)
        {
            this._dbContext = context;
            this._configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost]
        [Route("LoginAgent")]
        public async Task<IActionResult> Login([FromBody] Agent agent)
        {

            PasswordHasher<Agent> passwordHasher = new PasswordHasher<Agent>();
            var currentAgent = await _dbContext.Agents.Where(ev => ev.Email == agent.Email).FirstOrDefaultAsync();
            if (currentAgent != null)
            {
                if (passwordHasher.VerifyHashedPassword(agent, currentAgent.HashedPassword, agent.HashedPassword) == PasswordVerificationResult.Success)
                {
                    Shared shared = new Shared(_dbContext, _configuration);  //TODO Aim at doing this the more efficient way
                    return Ok(new { token = shared.GenerateToken(currentAgent), currentAgent});
                }
            }
            return null;
        }
    }
}
