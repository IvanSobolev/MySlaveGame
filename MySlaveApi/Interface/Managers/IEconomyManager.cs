namespace MySlaveApi.Interface;

public interface IEconomyManager
{
    public Task<float> GetCurrentSlavesPassiveIncomeAsync();
    public Task<float> GetCurrentSlavePassiveIncomeAsync(long id);
    public Task<float> TakeIncomeAsync();
    public Task<float> PriceOfASlaveAsync(long id);
}