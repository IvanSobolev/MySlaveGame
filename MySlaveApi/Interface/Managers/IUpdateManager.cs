using MySlaveApi.Model.ViewModel;

namespace MySlaveApi.Interface;

public interface IUpdateManager
{
    public Task<UserOutputDTO> RedeemYourselfAsync();
    public Task<UserOutputDTO> UpgradeSlaveAsync(long id);
    public Task<UserOutputDTO> UpgradeStorageLevel();
}