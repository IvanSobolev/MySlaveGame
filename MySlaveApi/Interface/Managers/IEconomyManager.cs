using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;

namespace MySlaveApi.Interface;

public interface IEconomyManager
{
    public Task<float> GetCurrentSlavesPassiveIncomeAsync();
    public Task<float> GetCurrentSlavePassiveIncomeAsync(long id);
    public Task<UserOutputDTO> TakeIncomeAsync();
    public Task<float> PriceOfASlaveAsync(long id);
}