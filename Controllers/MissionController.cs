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
                return NotFound("No missions were found for this agent");
            }

            return Ok(agentMissions);
        }

        [HttpPost]
        [Route("AddMission")]
        public async Task<IActionResult> AddMission([FromBody] Agent agent)
        {
            if (agent == null)
            {
                return BadRequest("Invalid Data");
            }

            bool agentExists = await _dbContext.Agents.AnyAsync(ev => ev.Id == agent.Id);
            if (!agentExists)
            {
                return NotFound("Agent not found");
            }

            Agent agentFromDB = await _dbContext.Agents.Where(ev => ev.Id == agent.Id).FirstOrDefaultAsync();

            Mission missionNew = new();
            missionNew.Id = Guid.NewGuid();
            missionNew.Name = "New mission";
            missionNew.Priority = "medium";
            missionNew.AgentsAssigned.Add(agentFromDB);
            _dbContext.Missions.Add(missionNew);

            List<Mission> missionsCurrent = agentFromDB.MissionsAssigned;
            missionsCurrent.Add(missionNew);

            await _dbContext.Agents.Where(ev => ev.Id == agent.Id).ExecuteUpdateAsync(setters => setters.SetProperty(ev => ev.MissionsAssigned, missionsCurrent));
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
