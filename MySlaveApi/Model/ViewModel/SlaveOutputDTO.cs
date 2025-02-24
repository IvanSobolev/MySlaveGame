namespace MySlaveApi.Model.ViewModel;

public class SlaveOutputDTO(User user)
{
    public long Id { get; set; } = user.Id;
    public string Username { get; set; } = user.Username;
    public int Level { get; set; } = user.Level;
}