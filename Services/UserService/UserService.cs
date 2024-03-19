using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.UserModels;

namespace SmartOffice.Services.UserService;

public class UserService : IUserService
{
    private readonly SmartOfficeDbContext _dbContext;
    private readonly IServiceProvider _service;

    public UserService(SmartOfficeDbContext dbContext, IServiceProvider service)
    {
        _dbContext = dbContext;
        _service = service;
    }
    
    public async Task SetUser(UserModel user, string plainTextPassword)
    {
        ArgumentNullException.ThrowIfNull(user);

        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(plainTextPassword);
        user.UserPassword = hashedPassword;

        _dbContext.SoUserTab.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public bool VerifyPassword(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
    }

    public async Task<UserModel> GetUserById(int userId)
    {
        return await _dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserId == userId) ?? throw new InvalidOperationException();
    }

    public async Task<UserModel> GetUserByUsername(string username)
    {
        return await _dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserName == username) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<UserModel>> GetAllUser()
    {
        return await _dbContext.SoUserTab.ToListAsync();
    }
}