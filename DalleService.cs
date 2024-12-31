using System.Text;
using System.Text.Json;

namespace GenAI.App
{
    public class DalleService : IImageGeneratorService
    {
        private readonly string _apiKey;
        private readonly string _apiUrl = "https://api.openai.com/v1/images/generations";

        public DalleService(string apiKey)
        {
            _apiKey = apiKey;
        }

        public async Task<string> GenerateImageAsync(string prompt)
        {
            using HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

            var requestBody = new
            {
                prompt = prompt,
                n = 1, // Number of images to generate
                size = "1024x1024" // Image resolution
            };

            var jsonRequest = JsonSerializer.Serialize(requestBody);
            var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(_apiUrl, content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var responseJson = JsonSerializer.Deserialize<DalleResponse>(responseBody);

                return responseJson?.Data?[0]?.Url ?? throw new Exception("No URL in response");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }

}
