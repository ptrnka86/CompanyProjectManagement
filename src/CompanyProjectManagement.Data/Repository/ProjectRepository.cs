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
 
            var xmlData = GetAllXMLData();

            var newProject = new ProjectDataModel();

            newProject.Id = GetNewId(xmlData);
            newProject.Name = model.Name;
            newProject.Abbrevation = model.Abbrevation;
            newProject.Customer = model.Customer;

            xmlData.ProjectList.Add(newProject);

            InsertXMLData(xmlData);

            return Task.FromResult(newProject.Id);
        }

        /// <summary>
        /// Check all ids and return max id increased by 1
        /// </summary>
        /// <param name="xmlData"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private string GetNewId(Projects xmlData)
        {
            var allIds = xmlData.ProjectList.Select(x => x.Id.Substring(3));
            int newId = 1;
            foreach (var id in allIds)
            {
                try {
                    int xmlId = Int32.Parse(id);
                    newId = xmlId >= newId ? xmlId + 1 : newId; 
                } catch  { 
                    throw new ArgumentOutOfRangeException($"Incorrect id '{ id }' in project.xml file");
                }
            }

            return "prj" + newId;
        }

        public Task DeleteAsync(string id)
        {
            var xmlData = GetAllXMLData();

            xmlData.ProjectList.RemoveAll(p => p.Id == id);

            InsertXMLData(xmlData);

            return Task.CompletedTask;
        }

        public async Task<List<ProjectModel>> GetAllAsync()
        {
            var xmlData = GetAllXMLData();

            var projectModels = new List<ProjectModel>();

            foreach ( var project in xmlData.ProjectList) 
            { 
                var projectModel = GetProjectModel(project);

                projectModels.Add(projectModel);
            }

            return projectModels;
        }

        public Task<ProjectModel> GetByIdAsync(string id)
        {
            
            var project = GetAllXMLData().ProjectList.FirstOrDefault(p => p.Id == id);

            if (project == null)
                throw new InvalidOperationException($"{ id } was not found");

            var projectModel = GetProjectModel(project);

            return Task.FromResult(projectModel);
        }

        public Task UpdateAsync(ProjectModel model)
        {
            var xmlData = GetAllXMLData();

            var project = xmlData.ProjectList.FirstOrDefault(p => p.Id == model.Id);

            if (project == null)
                throw new InvalidOperationException($"{model.Id} was not found");

            project.Name = model.Name;
            project.Abbrevation = model.Abbrevation;
            project.Customer = model.Customer;

            InsertXMLData(xmlData);

            return Task.CompletedTask;
        }

        private ProjectModel GetProjectModel(ProjectDataModel project)
        {
            var projectModel = new ProjectModel();
            projectModel.Id = project.Id;
            projectModel.Name = project.Name;
            projectModel.Abbrevation = project.Abbrevation;
            projectModel.Customer = project.Customer;

            return projectModel;
        }

        private Projects GetAllXMLData()
        {
            var xmlReader = GetXmlFileManager();

            return xmlReader.ReadData();
        }

        private void InsertXMLData(Projects model)
        {
            var xmlReader = GetXmlFileManager();

            xmlReader.WriteData(model);
        }

        private XmlFileManager<Projects> GetXmlFileManager()
        {
            string filePath = _configuration["AppSettings:XmlFilesPath"];
            string fileName = "Project.xml";
            string fullPath = filePath + "//" + fileName;
            return new XmlFileManager<Projects>(fullPath);
        }
    }
}
