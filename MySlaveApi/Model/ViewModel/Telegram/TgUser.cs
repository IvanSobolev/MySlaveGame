using System.Text.Json.Serialization;

namespace MySlaveApi.Model.ViewModel.Telegram;

public class TgUser(
    long id,
    string firstName,
    string lastName,
    string username,
    string languageCode,
    bool isPremium,
    bool allowWriteToPm,
    string photuUrl)
{
    [JsonPropertyName("id")]
    public long Id { get; set; } = id;

    [JsonPropertyName("first_name")]
    public string FirstName { get; set; } = firstName;

    [JsonPropertyName("last_name")]
    public string LastName { get; set; } = lastName;

    [JsonPropertyName("username")]
    public string Username { get; set; } = username;

    [JsonPropertyName("language_code")]
    public string LanguageCode { get; set; } = languageCode;

    [JsonPropertyName("is_premium")]
    public bool IsPremium { get; set; } = isPremium;

    [JsonPropertyName("allows_write_to_pm")]
    public bool AllowWriteToPm { get; set; } = allowWriteToPm;

    [JsonPropertyName("photo_url")]
    public string PhotuUrl { get; set; } = photuUrl;
}