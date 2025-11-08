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
    public class RegisterController : ControllerBase
    {
        private readonly CyberBoardDBContext _dbContext;

        public RegisterController(CyberBoardDBContext context)
        {
            _dbContext = context;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegisterAgentFresh([FromBody] Agent agent)
        {
            _dbContext.Agents.Add(CreateNewCommander(agent.Name, agent.HashedPassword));
            await _dbContext.SaveChangesAsync();
            return Ok("Agent created");
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAgentInAgency([FromBody] Agent agent)
        {
            if (agent == null) return BadRequest();

            bool existsInAgencyAlready = await _dbContext.Agents.AnyAsync(c => c.Name == agent.Name && c.AgencyId == agent.AgencyId);

            if(!existsInAgencyAlready)
            {
                Agent newAgentToAdd = CreateNewAgent(agent.Name, agent.HashedPassword);
                newAgentToAdd.Agency = await _dbContext.Agencies.Where(c => c.Id == agent.AgencyId).FirstOrDefaultAsync();
                newAgentToAdd.AgencyId = agent.AgencyId;
                _dbContext.Agents.Add(newAgentToAdd);
                await _dbContext.SaveChangesAsync();
                return CreatedAtAction("New agent", newAgentToAdd);
            }
            else
            {
                return BadRequest("Agent with this name already exists");
            }

        }

        [HttpPost]
        public async Task<IActionResult> RegisterAgency([FromBody] Agency agency)
        {
            if (agency == null || string.IsNullOrWhiteSpace(agency.Name))
            {
                return BadRequest("Invalid agency data");
            }

            bool exists = await _dbContext.Agencies.AnyAsync(c => c.Name == agency.Name);

            if (exists)
            {
                return Conflict("Agency with that name already exists");
            }

            agency.Id = Guid.NewGuid();
            agency.StartingDate = DateTime.UtcNow;

            _dbContext.Agencies.Add(agency);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction("Register Agency", agency);

        }

        private static Agent CreateNewAgent(string name, string password)
        {
            PasswordHasher<Agent> passwordHasher = new();
            Agent agent = new Agent();

            agent.Id = Guid.NewGuid();
            agent.Name = name;
            agent.Rank = Shared.Ranks[0];
            agent.Experience = 0;
            agent.HashedPassword = passwordHasher.HashPassword(agent, password);

            return agent;
        }

        private static Agent CreateNewCommander(string name, string password)
        {
            PasswordHasher<Agent> passwordHasher = new();
            Agent agent = new Agent();

            agent.Id = Guid.NewGuid();
            agent.Name = name;
            agent.Rank = Shared.Ranks[Shared.Ranks.Count - 1];
            agent.Experience = 0;
            agent.HashedPassword = passwordHasher.HashPassword(agent, password);

            return agent;
        }
    }
}
