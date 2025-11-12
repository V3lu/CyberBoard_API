using CyberBoardAPI.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CyberBoardAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MissionController : ControllerBase
    {
        private readonly CyberBoardDBContext _dbContext;

        public MissionController(CyberBoardDBContext context)
        {
            this._dbContext = context;
        }

        [HttpPost]
        [Route("GetAllAgentMissions")]
        public async Task<IActionResult> GetAllAgentMissions([FromBody] Agent agent)
        {
            if (agent == null)
            {
                return BadRequest("Invalid data");
            }

            bool agentExists = await _dbContext.Agents.AnyAsync(ev => ev.Id == agent.Id);
            if (!agentExists)
            {
                return NotFound("Agent not found");
            }

            List<Mission> agentMissions = await _dbContext.Missions.Where(ev => ev.AgentsAssigned.Any(iev => iev.Id == agent.Id)).ToListAsync();

            if (agentMissions.Count == 0)
            {
                return NotFound("No missions were foound for this agent");
            }

            return Ok(agentMissions);
        }
    }
}
