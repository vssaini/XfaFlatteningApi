namespace XfaFlatteningApi.Utils
{
    public class FileUtil
    {
        public static async Task<byte[]> GetFileBytes(IFormFile? file)
        {
            await using var memoryStream = new MemoryStream();
            if (file != null) 
                await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
