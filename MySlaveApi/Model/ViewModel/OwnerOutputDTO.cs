namespace MySlaveApi.Model.ViewModel;

public class OwnerOutputDTO (User user, double storageBalance, double maxStorageBalance)
{
    public long Id { get; set; } = user.Id;
    public string Username { get; set; } = user.Username;
    public User? Owner { get; set; } = user.Owner;
    public ICollection<SlaveOutputDTO> SubUsers { get; set; } = user.SubUsers.Select(user => new SlaveOutputDTO(user)).ToList();
    public double Balance { get; set; } = user.Balance;
    public double StorageBalance { get; set; } = storageBalance;
    public double MaxStorageBalance { get; set; } = maxStorageBalance;
}