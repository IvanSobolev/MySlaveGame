using Microsoft.EntityFrameworkCore;
using MySlaveApi.Data;
using MySlaveApi.Interface;
using MySlaveApi.Model;

namespace MySlaveApi.Repository;

public class TokenRepository (TokenContext tokenContext): ITokenRepository
{
    private readonly TokenContext _tokenContext = tokenContext;
    
    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        return await _tokenContext.RefreshTokens.FirstOrDefaultAsync(rt => rt.Token == token);
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        _tokenContext.RefreshTokens.Add(refreshToken);
        await _tokenContext.SaveChangesAsync();
    }

    public async Task DeleteByTokenAsync(string token)
    {
        var refreshToken = _tokenContext.RefreshTokens.FirstOrDefault(rt => rt.Token == token);
        if (refreshToken != null)
        {
            _tokenContext.RefreshTokens.Remove(refreshToken);
            await _tokenContext.SaveChangesAsync();
        }
    }

    public async Task DeleteByTgidAsync(long id)
    {
        var refreshToken = _tokenContext.RefreshTokens.FirstOrDefault(rt => rt.TgId == id);
        if (refreshToken != null)
        {
            _tokenContext.RefreshTokens.Remove(refreshToken);
            await _tokenContext.SaveChangesAsync();
        }
    }
}