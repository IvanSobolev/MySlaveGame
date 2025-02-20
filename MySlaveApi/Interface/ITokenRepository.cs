using MySlaveApi.Model;

namespace MySlaveApi.Interface;

public interface ITokenRepository
{
    public Task<RefreshToken?> GetByTokenAsync(string token);
    public Task AddAsync(RefreshToken refreshToken);
    public Task DeleteByTokenAsync(string token);
    public Task DeleteByTgidAsync(long id);
}