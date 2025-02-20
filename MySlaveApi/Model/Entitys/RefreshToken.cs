namespace MySlaveApi.Model;

public class RefreshToken
{
    public long TgId { get; set; }
    public string Token { get; set; }
    public DateTime Expires { get; set; }
    
}