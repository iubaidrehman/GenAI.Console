namespace GenAI.App
{
    public static class ImageGeneratorFactory
    {
        public static IImageGeneratorService GetService(string serviceType, string apiKey)
        {
            return serviceType.ToLower() switch
            {
                "dalle" => new DalleService(apiKey),
                // Add other services here in the future
                _ => throw new ArgumentException($"Service type {serviceType} is not supported.")
            };
        }
    }
}
