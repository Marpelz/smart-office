using SmartOffice.Models.UserModels;

namespace SmartOffice.Services.UserServices;

public interface IUserService
{
    Task SetUser(UserModel user, string plainTextPassword);
    bool VerifyPassword(string enteredPassword, string hashedPassword);
    Task<UserModel> GetUserById(int userId);
    Task<UserModel> GetUserByUsername(string username);
    Task<int> GetUserIdByUsername(string username);
    Task<string> GetUsernameById(int userId);
    Task<IEnumerable<UserModel>> GetAllUser();
    
}