using CompanyProjectManagement.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;
using USP.Controllers;

namespace CompanyProjectManagement.Controllers
{
    [ApiExplorerSettings(GroupName = "core")]
    [ApiController]
    [Route("api")]
    public class TokenController : BaseController
    {

        private readonly IUserProvider _userProvider;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly ILogger<TokenController> _logger;

        public TokenController(IUserProvider userProvider, ITokenGenerator tokenGenerator, ILogger<TokenController> logger)
        {
            _userProvider = userProvider;
            _tokenGenerator = tokenGenerator;
            _logger = logger;
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

                _logger.LogInformation($"User '{ username }' was successfully logged in");
                return Ok(new { access_token = token });
            }
            catch (Exception ex)
            {
                _logger.LogError($"User login error for '{username}'");
                return BadRequest(ex.Message);
            }
        }
    }
}
