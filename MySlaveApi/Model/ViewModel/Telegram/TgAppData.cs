using System.Text.Json.Serialization;

namespace MySlaveApi.Model.ViewModel.Telegram;

public class TgAppData
{
    [JsonPropertyName("user")]
    public TgUser User { get; set; }

    [JsonPropertyName("chat_instance")]
    public long ChatInstance { get; set; }
    
    [JsonPropertyName("chat_type")]
    public string ChatType { get; set; }

    [JsonPropertyName("auth_date")]
    public long AuthDate { get; set; }
    
    [JsonPropertyName("signature")]
    public string Signature { get; set; }

    [JsonPropertyName("hash")]
    public string Hash { get; set; }
}