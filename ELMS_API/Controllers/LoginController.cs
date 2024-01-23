using ELMS_API.Interfaces;
using ELMS_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ELMS_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public ActionResult Login(UserLogin user)
        {
            Employee employee = _loginService.Login(user.Email, user.Password);

            if (employee != null)
            {
                return Ok(employee);
            }
            return BadRequest(false);
        }
    }
}
