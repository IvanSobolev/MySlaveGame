namespace MySlaveApi.Interface;

public interface ITokenGeneratorService
{
    public string GenerateAccessToken(string tgId);
    public string GenerateRefreshToken(string tgId);
}