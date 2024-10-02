using Newtonsoft.Json;

namespace meditationApp.DTO.chat;

public class KeywordsResponse
{
    [JsonProperty("keywords")] public string? Keywords { get; set; }
}