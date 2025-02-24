using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MySlaveApi.Interface;

namespace MySlaveApi.Controller;

[ApiController]
[Route("[controller]")]
public class GameController(IGameManager gameManager) : ControllerBase
{
    private readonly IGameManager _gameManager = gameManager;

    [Authorize]
    [HttpGet("getPassiveIncome")]
    public async Task<IActionResult> GetCurrentSlavesPassiveIncomeAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _gameManager.GetCurrentSlavesPassiveIncomeAsync(Convert.ToInt64(id)));
    }
    
    [Authorize]
    [HttpGet("getSlavePassiveIncome/{slaveId}")]
    public async Task<IActionResult> GetCurrentSlavePassiveIncomeAsync(long slaveId)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _gameManager.GetCurrentSlavePassiveIncomeAsync(Convert.ToInt64(id), slaveId));
    }
    
    [Authorize]
    [HttpGet("takeIncome")]
    public async Task<IActionResult> TakeIncomeAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _gameManager.TakeIncomeAsync(Convert.ToInt64(id)));
    }
    
    [HttpGet("getSlavePrice/{id}")]
    public async Task<IActionResult> TakeIncomeAsync(long id)
    {
        return Ok(await _gameManager.PriceOfASlaveAsync(id));
    }
    
    [HttpGet("PriceOfUpgradeUser/{id}")]
    public async Task<IActionResult> PriceOfUpgradeUserAsync(long id)
    {   
        return Ok(await _gameManager.PriceOfUpgradeUserAsync(id));
    }
    
    [HttpGet("PriceOfUpgradeStorage/{id}")]
    public async Task<IActionResult> PriceOfUpgradeStorageAsync(long id)
    {
        return Ok(await _gameManager.PriceOfUpgradeStorageAsync(id));
    }
    
    [Authorize]
    [HttpGet("BuyNewSlave/{slaveId}")]
    public async Task<IActionResult> BuyNewSlave(long slaveId)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _gameManager.BuyNewSlave(Convert.ToInt64(id), slaveId);
        return user.SubUsers.FirstOrDefault(u => u.Id == slaveId) != null ? Ok(user) : BadRequest("not enough");
    }
    
    [Authorize]
    [HttpGet("RedeemYourself")]
    public async Task<IActionResult> RedeemYourselfAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _gameManager.RedeemYourselfAsync(Convert.ToInt64(id));
        return user.Owner != null? Ok(user) : BadRequest("not enough");
    }
    
    [Authorize]
    [HttpGet("UpgradeSlaveAsync/{slaveId}")]
    public async Task<IActionResult> UpgradeSlaveAsync(long slaveId)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _gameManager.UpgradeSlaveAsync(Convert.ToInt64(id), slaveId);
        return user.Level != -1 ? Ok(user) : BadRequest("not enough");
    }
    
    [Authorize]
    [HttpGet("UpgradeStorage")]
    public async Task<IActionResult> UpgradeStorageLevel()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var user = await _gameManager.UpgradeStorageLevel(Convert.ToInt64(id));
        return user.MaxStorageBalance != -1 ? Ok(user) : BadRequest("not enough");
    }
    
    [Authorize]
    [HttpGet("NewReferalUser/{referalId}")]
    public async Task<IActionResult> NewReferalUserAsync(long referalId)
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _gameManager.NewReferalUserAsync(Convert.ToInt64(id), referalId));
    }
    
    [Authorize]
    [HttpGet("SynchronizationAsync/")]
    public async Task<IActionResult> SynchronizationAsync()
    {
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        return Ok(await _gameManager.SynchronizationAsync(Convert.ToInt64(id)));
    }
}