using MySlaveApi.Model.ViewModel;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Interface;

public interface IAuthService
{
    public Task<TokenOutputDTO?> Auth(string initData);
    public Task<TokenOutputDTO?> RefreshToken(string refreshToken);
}