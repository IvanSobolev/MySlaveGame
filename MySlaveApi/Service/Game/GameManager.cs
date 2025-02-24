using MySlaveApi.Interface;
using MySlaveApi.Model;
using MySlaveApi.Model.ViewModel;

namespace MySlaveApi.Service.Game;

public class GameManager(IUserRepository userRepository) : IGameManager
{
    private readonly IUserRepository _userRepository = userRepository;

    public async Task<OwnerOutputDTO> NewReferalUserAsync(long authId, long referalId)
    {
        User user = await _userRepository.GetUserAsync(authId);
        if (user.Owner == null)
        {
            User newOwner = await _userRepository.GetUserAsync(referalId);
            user.Owner = newOwner;
            _ = _userRepository.UpdateUserAsync(user);
        }
        
        TimeSpan timeSpan = DateTime.UtcNow - user.LastTakeStamp;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        int maxStorageHours = (2 + user.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + user.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        
        return new OwnerOutputDTO(user, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
    }

    public async Task<OwnerOutputDTO> SynchronizationAsync(long authId)
    {
        User user = await _userRepository.GetUserAsync(authId);
        
        TimeSpan timeSpan = DateTime.UtcNow - user.LastTakeStamp;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        int maxStorageHours = (2 + user.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + user.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        
        return new OwnerOutputDTO(user, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
    }

    public async Task<double> GetCurrentSlavesPassiveIncomeAsync(long authId)
    {
        User user = await _userRepository.GetUserAsync(authId);
        double resultIncome = 0;
        if (user.SubUsers.Count == 0)
        {
            return resultIncome;
        }

        foreach (User slaves in user.SubUsers)
        {
            resultIncome += slaves.Level * 10;
        }

        return resultIncome;
    }

    public async Task<double> GetCurrentSlavePassiveIncomeAsync(long authId, long slaveId)
    {
        User user = await _userRepository.GetUserAsync(authId);
        
        double resultIncome = 0;
        if (user.SubUsers.Count == 0)
        { return resultIncome; }
        User slave = user.SubUsers.FirstOrDefault(u => u.Id == slaveId) ?? new User(-1);
        if (slave.Id == -1)
        { return resultIncome; }
        resultIncome = slave.Level * 10;
        return resultIncome;
    }

    public async Task<OwnerOutputDTO> TakeIncomeAsync(long authId)
    {
        User authUser = await _userRepository.GetUserAsync(authId);
        TimeSpan timeSpan = DateTime.UtcNow - authUser.LastTakeStamp;
        int maxStorageHours = (2 + authUser.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + authUser.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        double totalIncome = (incomePerHours / 3600) * totalSec;
        authUser.LastTakeStamp = DateTime.UtcNow;
        authUser.Balance += totalIncome;
        _ = _userRepository.UpdateUserAsync(authUser);
        return new OwnerOutputDTO(authUser, 0, incomePerHours * maxStorageHours);
    }

    public async Task<double> PriceOfUpgradeUserAsync(long slaveId)
    {
        User user = await _userRepository.GetUserAsync(slaveId);
        double price = 100 * (1 + 0.5 * (user.Level - 1));
        return price;
    }

    public async Task<double> PriceOfUpgradeStorageAsync(long authId)
    {
        User user = await _userRepository.GetUserAsync(authId);
        double price = 100 * (1 + 0.5 * (user.MaxStorageLevel - 1));
        return price;
    }

    public async Task<OwnerOutputDTO> BuyNewSlave(long authId, long id)
    {
        User authUser = await _userRepository.GetUserAsync(authId);
        User slaveUser = await _userRepository.GetUserAsync(id);

        TimeSpan timeSpan = DateTime.UtcNow - authUser.LastTakeStamp;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        int maxStorageHours = (2 + authUser.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + authUser.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        
        double price = await PriceOfASlaveAsync(id);

        if (authUser.Balance < price)
        {
            return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
        }

        authUser.Balance -= price;
        
        slaveUser.ResoldCount++;
        authUser.SubUsers.Add(slaveUser);
        slaveUser.Owner = authUser;
        slaveUser.OwnerId = authUser.Id;
        
        _ = _userRepository.UpdateUserAsync(slaveUser);
        _ = _userRepository.UpdateUserAsync(authUser);
        
        return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
    }

    public async Task<double> PriceOfASlaveAsync(long slaveId)
    {
        User user = await _userRepository.GetUserAsync(slaveId);
        double price = 120 * (1 + 0.5 * (user.Level - 1)) * (1 + 0.35 * user.ResoldCount);
        return price;
    }
    
    public async Task<OwnerOutputDTO> RedeemYourselfAsync(long authId)
    {
        User authUser = await _userRepository.GetUserAsync(authId);
        
        TimeSpan timeSpan = DateTime.UtcNow - authUser.LastTakeStamp;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        int maxStorageHours = (2 + authUser.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + authUser.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        
        if (authUser.Owner == null || authUser.OwnerId == null)
        {
            return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
        }
        
        double price = await PriceOfASlaveAsync(authId);

        if (authUser.Balance < price)
        {
            return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
        }

        authUser.Balance -= price;
        
        authUser.Owner.SubUsers.Remove(authUser);
        authUser.Owner = null;
        authUser.OwnerId = null;
        
        _ = _userRepository.UpdateUserAsync(authUser);
        
        return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
    }

    public async Task<SlaveOutputDTO> UpgradeSlaveAsync(long authId, long id)
    {
        User authUser = await _userRepository.GetUserAsync(authId);
        User slave = authUser.SubUsers.FirstOrDefault(u => u.Id == id);
        
        if (slave == null)
        {
            return new SlaveOutputDTO(new User(-1));
        }

        var price = await PriceOfUpgradeUserAsync(id);
        if (authUser.Balance < price)
        {
            slave.Level = -1;
            return new SlaveOutputDTO(slave);
        }
        
        authUser.Balance -= price;
        slave.Level++;
        
        _ = _userRepository.UpdateUserAsync(authUser);
        return new SlaveOutputDTO(slave);
    }

    public async Task<OwnerOutputDTO> UpgradeStorageLevel(long authId)
    {
        User authUser = await _userRepository.GetUserAsync(authId);

        TimeSpan timeSpan = DateTime.UtcNow - authUser.LastTakeStamp;
        double incomePerHours = await GetCurrentSlavesPassiveIncomeAsync(authId);
        int maxStorageHours = (2 + authUser.MaxStorageLevel);
        double totalSec = timeSpan.TotalHours > 2 + authUser.MaxStorageLevel? maxStorageHours * 3600: timeSpan.TotalSeconds;
        
        var price = await PriceOfUpgradeStorageAsync(authId);
        if (authUser.Balance < price)
        {
            return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, -1);
        }
        
        authUser.Balance -= price;
        authUser.MaxStorageLevel++;
        
        _ = _userRepository.UpdateUserAsync(authUser);
        return new OwnerOutputDTO(authUser, totalSec * incomePerHours / 3600, incomePerHours / maxStorageHours);
    }
}