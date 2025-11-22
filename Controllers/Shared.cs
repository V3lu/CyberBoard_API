using CyberBoardAPI.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CyberBoardAPI.Controllers
{
    public class Shared
    {
        public static List<string> Ranks = new List<string> { "Agent", "Arch Agent", "Cipher", "Arch Cipher", "Vex", "Arch Vex", "Ghost", "Arch Ghost", "Captain", "Arch Captain", "Commander" };

        private CyberBoardDBContext _dbContext;
        private IConfiguration _configuration;
        private string _secret = Environment.GetEnvironmentVariable("SECRET")!;

        public Shared(CyberBoardDBContext dBContext, IConfiguration config)
        {
            this._dbContext = dBContext;
            this._configuration = config;
        }

        public string GenerateToken(Agent agent)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, agent.Name),
                new Claim(ClaimTypes.Role, agent.Rank)
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
