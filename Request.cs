using System.Text.Json.Serialization;

namespace GenAI.App
{
    public record DalleRequest(string Prompt, int N = 1, string Size = "1024x1024");

    public record DalleResponse
    {
        [JsonPropertyName("data")]
        public ImageData[]? Data { get; init; }

        public record ImageData
        {
            [JsonPropertyName("url")]
            public string? Url { get; init; }
        }
    }
}
