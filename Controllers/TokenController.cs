using CompanyProjectManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CompanyProjectManagement.Controllers
{
    [ApiExplorerSettings(GroupName = "core")]
    [ApiController]
    [Route("api")]
    public class TokenController : ControllerBase
    {

        private readonly IUserProvider _userProvider;

        public TokenController(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        [Produces("application/json")]
        public async Task<IActionResult> Login([FromForm][Required] string username, [FromForm][Required] string password)
        {
            try
            {
                var user = _userProvider.LoginByUsernamePassAsync(username, password);
                // TODO: implementovat generovanie JWS tokenu

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
