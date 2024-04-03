using CompanyProjectManagement.Data.Model;
using CompanyProjectManagement.Data.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyProjectManagement.Data.Repository
{
    public interface IProjectRepository
    {
        Task<List<ProjectModel>> GetAllAsync();
        Task<ProjectModel> GetByIdAsync(string id);
        Task<string> CreateAsync(NewProjectRequestModel model);
        Task UpdateAsync(ProjectModel model);
        Task DeleteAsync(string id);
    }
    public class ProjectRepository : IProjectRepository
    {
        private readonly IConfiguration _configuration;

        public ProjectRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Task<string> CreateAsync(NewProjectRequestModel model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectModel>> GetAllAsync()
        {
            string filePath = _configuration["AppSettings:XmlFilesPath"];
            string fileName = "Project.xml";
            string fullPath = filePath + "//" +  fileName;
            var xmlReader = new XmlFileManager<Projects>(fullPath);

            var xmlData = xmlReader.ReadData();

            var projectModels = new List<ProjectModel>();

            foreach ( var project in xmlData.Project) 
            { 
                var projectModel = new ProjectModel();
                projectModel.Id = project.Id;
                projectModel.Name = project.Name;
                projectModel.Abbrevation = project.Abbrevation;
                projectModel.Customer = project.Customer;

                projectModels.Add(projectModel);
            }

            return projectModels;
        }

        public Task<ProjectModel> GetByIdAsync(string id)
        {
            string filePath = _configuration["AppSettings:XmlFilesPath"];
            string fileName = "Project.xml";
            string fullPath = filePath + "//" + fileName;
            var xmlReader = new XmlFileManager<Projects>(fullPath);

            var xmlData = xmlReader.ReadData();
            var project = xmlData.Project.FirstOrDefault(p => p.Id == id);

            if (project == null)
                throw new InvalidOperationException($"{ id } was not found");


            var projectModel = new ProjectModel();
            projectModel.Id = project.Id;
            projectModel.Name = project.Name;
            projectModel.Abbrevation = project.Abbrevation;
            projectModel.Customer = project.Customer;

            return Task.FromResult(projectModel);
        }

        public Task UpdateAsync(ProjectModel model)
        {
            throw new NotImplementedException();
        }
    }
}
