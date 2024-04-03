using CompanyProjectManagement.Data.Model;
using CompanyProjectManagement.Data.Repository;
using Microsoft.AspNetCore.Identity;

namespace CompanyProjectManagement.Security
{
    public interface IUserProvider
    {
        Task<UserModel> LoginByUsernamePassAsync(string username, string password);
    }
    public class UserProvider : IUserProvider
    {
        private readonly IUserRepository _userRepository;
        public UserProvider(IUserRepository userRepository)
        {
                _userRepository = userRepository;
        }

        public async Task<UserModel> LoginByUsernamePassAsync(string username, string password)
        {
            if(username == null)
                throw new ArgumentNullException(nameof(username));

            if(password == null)
                throw new ArgumentNullException(nameof(password));

            // TODO: implementovat nacitanie uzivatelov z xml suboru
            if(password != await _userRepository.GetPasswordByUserNameAsync(username))
                throw new ArgumentException("Incorrect userName or password");
            
            return await _userRepository.GetByUserNameAsync(username);
        }
    }
}
