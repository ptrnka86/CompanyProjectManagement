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
    public interface IUserRepository
    {
        Task<string> GetPasswordByUserNameAsync(string userName);
        Task<UserModel> GetByIdAsync(Guid id);
        Task<UserModel> GetByUserNameAsync(string userName);

    }
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _configuration;

        public UserRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Task<string> GetPasswordByUserNameAsync(string userName)
        {
            var xmlData = GetAllXMLData();

            var user = xmlData.UserList.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                throw new InvalidOperationException($"{userName} was not found");
            }

            return Task.FromResult(user.Password);
        }

        public Task<UserModel> GetByIdAsync(Guid id)
        {
            
            var user = GetAllXMLData().UserList.FirstOrDefault(u => u.UserId == id);

            if (user == null)
                throw new InvalidOperationException($"user with id '{id}' was not found");

            var userModel = new UserModel();
            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;

            return Task.FromResult(userModel);
        }

        public Task<UserModel> GetByUserNameAsync(string userName)
        {
            var user = GetAllXMLData().UserList.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
                throw new InvalidOperationException($"{userName} was not found");

            var userModel = new UserModel();
            userModel.UserId = user.UserId;
            userModel.UserName = user.UserName;

            return Task.FromResult(userModel);
        }

        private Users GetAllXMLData()
        {
            var xmlReader = GetXmlFileManager();

            return xmlReader.ReadData();
        }

        private XmlFileManager<Users> GetXmlFileManager()
        {
            string filePath = _configuration["AppSettings:XmlFilesPath"];
            string fileName = "User.xml";
            string fullPath = filePath + "//" + fileName;
            return new XmlFileManager<Users>(fullPath);
        }
    }
}
