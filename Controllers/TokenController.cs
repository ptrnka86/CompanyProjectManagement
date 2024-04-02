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
        private readonly ITokenGenerator _tokenGenerator;

        public TokenController(IUserProvider userProvider, ITokenGenerator tokenGenerator)
        {
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
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
                string token = _tokenGenerator.GenerateTokenSync(user);
                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
