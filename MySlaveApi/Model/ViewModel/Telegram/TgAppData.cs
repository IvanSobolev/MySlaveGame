using System.Text.Json.Serialization;

namespace MySlaveApi.Model.ViewModel.Telegram;

public class TgAppData
{
    [JsonPropertyName("query_id")]
    public string? QueryId { get; set; }

    [JsonPropertyName("user")]
    public TgUser? User { get; set; }

    [JsonPropertyName("auth_date")]
    public long AuthDate { get; set; }

    [JsonPropertyName("hash")]
    public string? Hash { get; set; }
}