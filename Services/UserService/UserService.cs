using Microsoft.EntityFrameworkCore;
using SmartOffice.Models.UserModels;

namespace SmartOffice.Services.UserService;

public class UserService(SmartOfficeDbContext dbContext) : IUserService
{
    public async Task SetUser(UserModel user, string plainTextPassword)
    {
        ArgumentNullException.ThrowIfNull(user);

        dbContext.SoUserTab.Add(user);
        await dbContext.SaveChangesAsync();
    }

    public async Task<UserModel> GetUserById(int userId)
    {
        return await dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserId == userId) ?? throw new InvalidOperationException();
    }

    public async Task<UserModel> GetUserByUsername(string username)
    {
        return await dbContext.SoUserTab.FirstOrDefaultAsync(u => u.UserName == username) ?? throw new InvalidOperationException();
    }

    public async Task<IEnumerable<UserModel>> GetAllUser()
    {
        return await dbContext.SoUserTab.ToListAsync();
    }
    
    /* Example to hash and safe entered Password
     
    public string HashPassword(string password, byte[] salt)
    {
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
        {
            byte[] hash = pbkdf2.GetBytes(32);

            byte[] hashBytes = new byte[48];
            Array.Copy(salt, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 32);

            return Convert.ToBase64String(hashBytes);
        }
    }

    private byte[] GenerateSalt()
    {
        var salt = new byte[16];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }
    */
}