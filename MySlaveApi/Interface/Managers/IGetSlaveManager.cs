using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Interface;

public interface IGetSlaveManager
{
    public Task NewReferalUserAsync(TgUser user, long referalId);
    public Task BuyNewSlave(long id);
}