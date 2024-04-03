using CompanyProjectManagement.Data.Model;
using CompanyProjectManagement.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using USP.Controllers;

namespace CompanyProjectManagement.Controllers
{
    [ApiExplorerSettings(GroupName = "project")]
    [ApiController]
    [Authorize]
    [Route("api/project")]
    public class ProjectController : BaseController
    {
        private readonly IProjectService _projectService;

        public ProjectController(ILogger<TokenController> logger, IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        [Route("get/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _projectService.GetByIdAsync(id));
        }

        [HttpGet]
        [Route("getAll")]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetAllAsync());   
        }

        [HttpPut]
        [Route("update")]
        [Produces("application/json")]
        public async Task<IActionResult> Update(ProjectModel model)
        {
            return Ok(await _projectService.UpdateAsync(model));
        }

        [HttpPost]
        [Route("create")]
        [Produces("application/json")]
        public async Task<IActionResult> Create(NewProjectRequestModel model)
        {
            return Ok(await _projectService.CreateAsync(model));
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Produces("application/json")]
        public async Task<IActionResult> Delete(string id)
        {
            await _projectService.DeleteAsync(id);
            return Ok();
        }
    }
}
