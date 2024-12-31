namespace GenAI.App
{
    internal class Program
    {
        private const string ApiKey = $"";

        static async Task Main(string[] args)
        {
            Console.WriteLine("Welcome to the AI Image Generator!");
            Console.Write("Enter the image generation service (e.g., DALL·E): ");
            string serviceType = Console.ReadLine();

            Console.Write("Enter a prompt for your image: ");
            string prompt = Console.ReadLine();

            if (string.IsNullOrEmpty(serviceType) || string.IsNullOrEmpty(ApiKey))
            {
                Console.WriteLine("key or Service cannot be null!");
                Environment.Exit(0);
            }
            try
            {
                var imageGeneratorService = ImageGeneratorFactory.GetService(serviceType, ApiKey);
                Console.WriteLine("Generating your image... Please wait.");
                string imageUrl = await imageGeneratorService.GenerateImageAsync(prompt);

                if (!string.IsNullOrEmpty(imageUrl))
                {
                    Console.WriteLine($"Image generated successfully! Here is the URL:\n{imageUrl}");

                    // Save URL and prompt to a file
                    string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                    string filePath = $"generated_image_details_{timestamp}.txt";
                    string fileContent = $"Prompt: {prompt}\nImage URL: {imageUrl}\n";
                    File.WriteAllText(filePath, fileContent);
                    Console.WriteLine($"Details saved to {filePath}");

                    // Download the image
                    //await DownloadImageAsync(imageUrl);
                }
                else
                {
                    Console.WriteLine("Failed to generate the image. Please try again.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static async Task DownloadImageAsync(string imageUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Fetch image from URL
                    byte[] imageBytes = await client.GetByteArrayAsync(imageUrl);

                    // Save image to disk
                    string imagePath = "generated_image.jpg";
                    await File.WriteAllBytesAsync(imagePath, imageBytes);
                    Console.WriteLine($"Image downloaded and saved as {imagePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error downloading the image: {ex.Message}");
                }
            }
        }
    }
}
