using System.Text.Json.Serialization;

namespace MySlaveApi.Model.ViewModel.Telegram;

public class TgUser
{
    [JsonPropertyName("id")]
    public long Id { get; set; }

    [JsonPropertyName("first_name")]
    public string? FirstName { get; set; }

    [JsonPropertyName("last_name")]
    public string? LastName { get; set; }

    [JsonPropertyName("username")]
    public string Username { get; set; }

    [JsonPropertyName("language_code")]
    public string? LanguageCode { get; set; }
    
    [JsonPropertyName("is_premium")]
    public bool? IsPremium { get; set; }
    
    [JsonPropertyName("allows_write_to_pm")]
    public bool? AllowWriteToPm { get; set; }
}