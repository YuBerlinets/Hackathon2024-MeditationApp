using Newtonsoft.Json;

namespace meditationApp.DTO.chat;

public class ProblemDescription
{
    [JsonProperty("description")] public string description { get; set; }
}