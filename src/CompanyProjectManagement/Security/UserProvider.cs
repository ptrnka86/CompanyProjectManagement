using CompanyProjectManagement.Data.Model;
using Microsoft.AspNetCore.Identity;

namespace CompanyProjectManagement.Security
{
    public interface IUserProvider
    {
        UserModel LoginByUsernamePassAsync(string username, string password);
    }
    public class UserProvider : IUserProvider
    {
        public UserModel LoginByUsernamePassAsync(string username, string password)
        {
            if(username == null)
                throw new ArgumentNullException(nameof(username));

            if(password == null)
                throw new ArgumentNullException(nameof(password));

            // TODO: implementovat nacitanie uzivatelov z xml suboru
            if (password != "demo123" || username != "demo")
                throw new ArgumentException("Incorrect userName or password");
            
            return new UserModel
            {
                UserId = new Guid("b6a33530-4c40-4b99-b9e9-989fc8157e59"),
                UserName = username,
            };
        }
    }
}
