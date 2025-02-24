using MySlaveApi.Interface;
using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Service;

public class AuthService(ITokenGeneratorService tokenGeneratorService, ITokenRepository tokenRepository, IUserRepository userRepository) : IAuthService
{
    private readonly ITokenGeneratorService _tokenGeneratorService = tokenGeneratorService;
    private readonly ITokenRepository _tokenRepository = tokenRepository;
    private readonly IUserRepository _userRepository = userRepository;
    private readonly TelegramAuthService _telegramAuthService = new TelegramAuthService("../tg.token");
    
    public async Task<TokenOutputDTO?> Auth(TgAppData data)
    {
        if (!await _telegramAuthService.ValidateTelegramData(data))
        {
            return null;
        }
        
        User user = await _userRepository.GetUserAsync(data.User.Id);
        if (user.Id == -1)
        {
            user = new User (data.User.Id)
            {
                Username = data.User.Username,
                OwnerId = null,
                Owner = null,
                Level = 1,
                ResoldCount = 0,
                Balance = 100,
                MaxStorageLevel = 1,
                LastTakeStamp = DateTime.UtcNow
                
            };
            Console.WriteLine(user.Username);
            await _userRepository.AddUserAsync(user);
        }
        
        await _tokenRepository.DeleteByTgidAsync(user.Id);
        
        var accessToken = _tokenGeneratorService.GenerateAccessToken(user.Id.ToString());
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken(user.Id.ToString());
        
        await _tokenRepository.AddAsync(new RefreshToken
        {
            TgId = user.Id,
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddHours(0.5)
        });

        return new TokenOutputDTO(accessToken, refreshToken);
    }


    public async Task<TokenOutputDTO?> RefreshToken(string token)
    {
        var refreshToken = await _tokenRepository.GetByTokenAsync(token);

        if (refreshToken == null || refreshToken.Expires < DateTime.UtcNow)
        {
            return null;
        }

        _ = _tokenRepository.DeleteByTokenAsync(token);

        var newAccessToken = _tokenGeneratorService.GenerateAccessToken(refreshToken.TgId.ToString());
        var newRefreshToken = _tokenGeneratorService.GenerateRefreshToken(refreshToken.TgId.ToString());

        _ = _tokenRepository.AddAsync(new RefreshToken
        {
            Token = newRefreshToken,
            TgId = refreshToken.TgId,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return new TokenOutputDTO(newAccessToken, newRefreshToken);
    }
}