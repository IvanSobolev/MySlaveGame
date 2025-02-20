namespace MySlaveApi.Model.ViewModel;

public class TokenOutputDTO(string accessToken, string refreshToken)
{
    public string AccessToken { get; set; } = accessToken;
    public string RefreshToken { get; set; } = refreshToken;
}