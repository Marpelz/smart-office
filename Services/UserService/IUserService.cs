using SmartOffice.Models.UserModels;

namespace SmartOffice.Services.UserService;

public interface IUserService
{
    Task SetUser(UserModel user, string plainTextPassword);
    bool VerifyPassword(string enteredPassword, string hashedPassword);
    Task<UserModel> GetUserById(int userId);
    Task<UserModel> GetUserByUsername(string username);
    Task<IEnumerable<UserModel>> GetAllUser();
    
}