using CompanyProjectManagement.Model;

namespace CompanyProjectManagement.Repository
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(string id);
        Task<string> CreateAsync(NewProjectRequestModel model);
        Task UpdateAsync(ProjectModel model);
        Task DeleteAsync(string id);
    }
}
