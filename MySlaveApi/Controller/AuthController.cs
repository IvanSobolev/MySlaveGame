using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySlaveApi.Interface;
using MySlaveApi.Model.ViewModel.Telegram;

namespace MySlaveApi.Controller;

[ApiController]
[Route("[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    private IAuthService _authService = authService;

    [HttpPost("auth/")]
    public async Task<IActionResult> TelegrammAuth([FromBody]TgAppData data)
    { 
        var tokens = await _authService.Auth(data);
        if (tokens == null)
        {
            return Unauthorized("telegramm string is not correct");
        }

        return Ok(tokens);
    }
    
    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshToken([FromBody]string token)
    { 
        var tokens = await _authService.RefreshToken(token);
        if (tokens == null)
        {
            return Unauthorized("Token is not active");
        }

        return Ok(tokens);
    }

    [Authorize]
    [HttpGet("authTest")]
    public IActionResult AuthTest()
    {
        return Ok("ITS WORK");
    }
}