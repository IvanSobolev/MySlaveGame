namespace MySlaveApi.Interface;

public interface IUpdateManager
{
    public Task RedeemYourselfAsync();
    public Task UpgradeSlaveAsync(long id);
    public Task UpgradeStorageLevel();
}