using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ELMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IManagerService _managerService;
        private IConfiguration _config;

        public LoginController(ILoginService loginService, IConfiguration config,IManagerService managerService)
        {
            _loginService = loginService;
            _config = config;
            _managerService = managerService;
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            try
            {
                Employee employee = _loginService.Login(user.Email, user.Password);

                if (employee != null)
                {
                    var issuer = _config["Jwt:Issuer"];
                    var audience = _config["Jwt:Audience"];
                    var key = Encoding.ASCII.GetBytes
                    (_config["Jwt:Key"]);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new[]
                        {
                            new Claim("Id", employee.EmployeeId.ToString()),
                            new Claim("ManagerId",_managerService.GetManagerIdByEmployeeId(employee.EmployeeId).ToString()),
                            new Claim("Role",employee.Role),
                            new Claim(JwtRegisteredClaimNames.Sub, employee.FirstName+' '+employee.LastName),
                            new Claim(JwtRegisteredClaimNames.Email, employee.Email),
                            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                        }),
                        Expires = DateTime.UtcNow.AddMinutes(5),
                        Issuer = issuer,
                        Audience = audience,
                        SigningCredentials = new SigningCredentials
                        (new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature)
                    };
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var jwtToken = tokenHandler.WriteToken(token);
                    var stringToken = tokenHandler.WriteToken(token);
                    return Ok(stringToken);
                }
                return StatusCode(404);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
