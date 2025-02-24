namespace MySlaveApi.Model;

public class User (long id)
{
    public long Id { get; set; } = id;
    public string Username { get; set; } = string.Empty;
    public long? OwnerId { get; set; }
    public User? Owner { get; set; }
    public ICollection<User> SubUsers { get; set; } = new List<User>();
    
    public int Level { get; set; }
    public int ResoldCount { get; set; }
    public int Balance { get; set; }
    public int MaxStorageLevel { get; set; }
    public DateTime LastTakeStamp { get; set; }
    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}