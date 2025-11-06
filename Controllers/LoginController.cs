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
        public async Task<Agent> Login([FromBody] Agent agent)
        {

            PasswordHasher<Agent> passwordHasher = new PasswordHasher<Agent>();
            var currentUser = await _dbContext.Agents.FirstOrDefaultAsync(x => x.Name.ToLower() ==
            agent.Name.ToLower());
            if (currentUser != null)
            {
                if (passwordHasher.VerifyHashedPassword(agent, currentUser.HashedPassword, agent.HashedPassword) == PasswordVerificationResult.Success)
                {
                    return currentUser;
                }
            }
            return null;
        }
    }
}
