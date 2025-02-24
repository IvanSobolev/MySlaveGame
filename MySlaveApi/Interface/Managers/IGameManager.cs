using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;

namespace MySlaveApi.Interface;

public interface IGameManager
{
    public Task<OwnerOutputDTO> NewReferalUserAsync(long authId, long referalId);
    public Task<OwnerOutputDTO> SynchronizationAsync(long authId);
    public Task<double> GetCurrentSlavesPassiveIncomeAsync(long authId);
    public Task<double> GetCurrentSlavePassiveIncomeAsync(long authId, long slaveId);
    public Task<OwnerOutputDTO> TakeIncomeAsync(long authId);
    public Task<double> PriceOfASlaveAsync(long slaveId);
    public Task<double> PriceOfUpgradeUserAsync(long slaveId);
    public Task<double> PriceOfUpgradeStorageAsync(long authId);
    public Task<OwnerOutputDTO> BuyNewSlave(long authId, long id);
    public Task<OwnerOutputDTO> RedeemYourselfAsync(long authId);
    public Task<SlaveOutputDTO> UpgradeSlaveAsync(long authId, long id);
    public Task<OwnerOutputDTO> UpgradeStorageLevel(long authId);
}