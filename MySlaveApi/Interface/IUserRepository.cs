using MySlaveApi.Model;

namespace MySlaveApi.Interface;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetUsersAsync();
    public Task<User> GetUserAsync(long id);
    public Task AddUserAsync(User user);
    public Task UpdateUserAsync(User updatedUser);
    public Task DeleteUserAsync(long id);
}