using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.UserModels;

namespace SmartOffice.Services.UserServices;

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
        return await _dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserId == userId)
               ?? throw new InvalidOperationException();
    }

    public async Task<UserModel> GetUserByUsername(string username)
    {
        return await _dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserName == username)
               ?? throw new InvalidOperationException();
    }
    
    public async Task<int> GetUserIdByUsername(string username)
    {
        var user = await _dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserName == username);
        return user?.UserId ?? throw new InvalidOperationException("Benutzer nicht gefunden");
    }
    
    public async Task<string> GetUsernameById(int userId)
    {
        try
        {
            var user = await _dbContext.SoUserTab.FindAsync(userId);
            return user?.UserName ?? throw new InvalidOperationException("Benutzer nicht gefunden"); 
        }
        catch (Exception ex)
        {
            throw new Exception("Fehler beim Abrufen des Benutzernamens nach Benutzer-ID", ex);
        }
    }

    public async Task<string> GetUserPaypalEmailById(int userId)
    {
        try
        {
            var user = await _dbContext.SoUserTab.FindAsync(userId);
            return user?.PaypalEmail ?? throw new InvalidOperationException("Paypal Adresse nicht gefunden"); 
        }
        catch (Exception ex)
        {
            throw new Exception("Fehler beim Abrufen der Paypal Adresse nach Benutzer-ID", ex);
        }
    }

    public async Task<IEnumerable<UserModel>> GetAllUser()
    {
        return await _dbContext.SoUserTab.ToListAsync();
    }
}