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

        public LoginController(CyberBoardDBContext context)
        {
            this._dbContext = context;
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
                    return Ok(currentAgent);
                }
            }
            return null;
        }
    }
}
