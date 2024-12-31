namespace GenAI.App
{
    public interface IImageGeneratorService
    {
        Task<string> GenerateImageAsync(string prompt);
    }
}
