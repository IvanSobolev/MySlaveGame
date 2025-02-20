using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Interface;

public interface IGetSlaveManager
{
    public Task<bool> NewReferalUserAsync(TgUser user, long referalId);
    public Task<UserOutputDTO> BuyNewSlave(long id);
}