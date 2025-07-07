using Microsoft.EntityFrameworkCore;
using ShowTime.DataAccess.Models;
using ShowTime.DataAccess.Repositories.Abstractions;

namespace ShowTime.DataAccess.Repositories.Implementations;

public class UserRepository : GenericRepository<User>, IUserRepository
{
    private readonly DbSet<User> _users;

    public UserRepository(ShowtimeDbContext context) : base(context)
    {
        _users = context.Set<User>();
    }
    
    public async Task<User?> GetByEmail(string email)
    {
        try
        {
            Console.WriteLine($"Repository: Looking for email: '{email}'");
        
            var user = await _users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Email == email);
            
            if (user == null)
            {
                Console.WriteLine("Exact match failed, trying normalized search...");
                user = await _users
                    .Include(u => u.Role)
                    .SingleOrDefaultAsync(u => u.Email.Trim().ToLower() == email.Trim().ToLower());
            }
        
            Console.WriteLine($"Repository: Found user: {user != null}");
            return user;
        }
        catch (Exception ex)
        {
            throw new Exception($"Unable to retrieve user with email {email}: {ex.Message}");
        }
    }
}