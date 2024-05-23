namespace XfaFlatteningApi.Contracts;

public interface IFlattenerUtil
{
    byte[]? GetFlattenedBytes(byte[] inputFileBytes);
    Task<byte[]> GetFlattenedBytesAsync(IFormFile? file);

    /// <summary>
    /// Converts the XFA PDF to a flattened PDF. This doesn't work and ask for licensed Foxit library.
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    Task<byte[]> GetFlattenedXfaPdfBytesAsync(IFormFile? file);
}