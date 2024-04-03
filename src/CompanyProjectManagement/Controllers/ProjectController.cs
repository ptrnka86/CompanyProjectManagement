using CompanyProjectManagement.Data.Model;
using CompanyProjectManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USP.Controllers;

namespace CompanyProjectManagement.Controllers
{
    [ApiExplorerSettings(GroupName = "project")]
    [ApiController]
    //[Authorize]
    [Route("api/project")]
    public class ProjectController : BaseController
    {
        private readonly ILogger<TokenController> _logger;
        private readonly IProjectService _projectService;

        public ProjectController(ILogger<TokenController> logger, IProjectService projectService)
        {
            _logger = logger;
            _projectService = projectService;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            try
            {
                return Ok(await _projectService.GetByIdAsync(id));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpGet]
        [Route("getAll")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                return Ok(await _projectService.GetAllAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpPut]
        [Route("update")]
        [Produces("application/json")]
        public async Task<IActionResult> Update(ProjectModel model)
        {
            try
            {
                return Ok(await _projectService.UpdateAsync(model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<IActionResult> Create(NewProjectRequestModel model)
        {
            try
            {
                return Ok(await _projectService.CreateAsync(model));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _projectService.DeleteAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing the request.");
            }
        }
    }
}
