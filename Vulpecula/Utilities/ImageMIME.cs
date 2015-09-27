namespace Vulpecula.Utilities
{
    internal static class ImageMime
    {
        public static string GetMimeType(string path)
        {
            if (path.ToLower().EndsWith(".png"))
                return "image/png";
            if (path.ToLower().EndsWith(".gif"))
                return "image/gif";
            return "image/jpeg";
        }
    }
}