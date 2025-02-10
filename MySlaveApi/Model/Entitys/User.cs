namespace MySlaveApi.Model;

public class User
{
    public long Id { get; set; }
    public string UserName { get; set; }
    public long? OwnerId { get; set; }
    public User Owner { get; set; }
    public ICollection<User> SubUsers { get; set; } = new List<User>();
    
    public int Level { get; set; }
    public int ResoldCount { get; set; }
}