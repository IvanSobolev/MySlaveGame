using Microsoft.EntityFrameworkCore;
using MySlaveApi.Data;
using MySlaveApi.Interface;
using MySlaveApi.Model;

namespace MySlaveApi.Repository;

public class UserRepository (DataContext dataContext) : IUserRepository
{
    private readonly DataContext _dataContext = dataContext;
    
    public async Task<IEnumerable<User>> GetUsersAsync()
    {
        return await _dataContext.Users.ToListAsync();
    }

    public async Task<User> GetUserAsync(long id)
    {
        return await _dataContext.Users.FindAsync(id) ?? new User(-1);
    }

    public async Task AddUserAsync(User user)
    {
        await _dataContext.Users.AddAsync(user);
        await _dataContext.SaveChangesAsync();
    }

    public async Task UpdateUserAsync(User updatedUser)
    {
        var existingUser = await _dataContext.Users.FindAsync(updatedUser.Id);
        if (existingUser != null)
        {
            _dataContext.Entry(existingUser).CurrentValues.SetValues(updatedUser);
            await _dataContext.SaveChangesAsync();
        }
    }

    public async Task DeleteUserAsync(long id)
    {
        var existUser = await _dataContext.Users.FindAsync(id);
        if (existUser != null)
        {
            _dataContext.Users.Remove(existUser);
        }
    }
}