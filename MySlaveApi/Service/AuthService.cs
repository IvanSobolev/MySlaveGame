using MySlaveApi.Interface;
using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Service;

public class AuthService(ITokenGeneratorService tokenGeneratorService, ITokenRepository tokenRepository, IUserRepository userRepository) : IAuthService
{
    private ITokenGeneratorService _tokenGeneratorService = tokenGeneratorService;
    private ITokenRepository _tokenRepository = tokenRepository;
    private IUserRepository _userRepository = userRepository;
    private TelegramAuthService _telegramAuthService = new TelegramAuthService("");
    
    public async Task<TokenOutputDTO?> Auth(string initData)
    {
        TgUser loginUser = _telegramAuthService.ExtractUserData(initData);
        if (!await _telegramAuthService.ValidateTelegramData(initData))
        {
            return null;
        }
        
        User user = await _userRepository.GetUserAsync(loginUser.Id);
        if (user.Id == -1)
        {
            // TO DO REGISTER LOGIC
        }
        
        await _tokenRepository.DeleteByTgidAsync(user.Id);
        
        var accessToken = _tokenGeneratorService.GenerateAccessToken(user.Id.ToString());
        var refreshToken = _tokenGeneratorService.GenerateRefreshToken(user.Id.ToString());
        
        await _tokenRepository.AddAsync(new RefreshToken
        {
            TgId = user.Id,
            Token = refreshToken,
            Expires = DateTime.UtcNow.AddDays(7)
        });

        return new TokenOutputDTO(accessToken, refreshToken);
    }


    public Task<TokenOutputDTO?> RefreshToken(string refreshToken)
    {
        // TO DO REFRESH TOKEN LOGIC
        throw new NotImplementedException();
    }
}