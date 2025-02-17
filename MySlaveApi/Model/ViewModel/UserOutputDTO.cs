namespace MySlaveApi.Model.ViewModel;

public class UserOutputDTO
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public long? OwnerId { get; set; }
    public User Owner { get; set; }
    public ICollection<UserOutputDTO> SubUsers { get; set; } = new List<UserOutputDTO>();
    public int Balance { get; set; }
    public int StorageBalance { get; set; }
    public int MaxStorageBalance { get; set; }
}