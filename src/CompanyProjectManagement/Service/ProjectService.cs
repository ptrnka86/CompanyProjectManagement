using CompanyProjectManagement.Data.Repository;
using CompanyProjectManagement.Data.Model;
using Microsoft.IdentityModel.Tokens;

namespace CompanyProjectManagement.Service
{
    public interface IProjectService
    {
        Task<List<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(string id);
        Task<ProjectModel> CreateAsync(NewProjectRequestModel model);
        Task<ProjectModel> UpdateAsync(ProjectModel model);
        Task DeleteAsync(string id);
    }
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }
        public async Task<ProjectModel> CreateAsync(NewProjectRequestModel model)
        {
            if(model.Abbrevation.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(model.Abbrevation));
            if (model.Customer.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(model.Customer));

            string id = await _projectRepository.CreateAsync(model);

            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task DeleteAsync(string id)
        {
            if(id.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(id));

            await _projectRepository.DeleteAsync(id);
        }

        public Task<List<ProjectModel>> GetAllAsync()
        {
            return _projectRepository.GetAllAsync();
        }

        public async Task<ProjectModel> GetByIdAsync(string id)
        {
            if (id.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(id));

            return await _projectRepository.GetByIdAsync(id);
        }

        public async Task<ProjectModel> UpdateAsync(ProjectModel model)
        {
            if (model.Id.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(model.Id));
            if (model.Abbrevation.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(model.Abbrevation));
            if (model.Customer.IsNullOrEmpty())
                throw new ArgumentNullException(nameof(model.Customer));

            return await _projectRepository.GetByIdAsync(model.Id);
        }
    }
}
