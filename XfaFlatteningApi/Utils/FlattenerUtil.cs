using foxit;
using foxit.addon.xfa;
using foxit.common;
using foxit.pdf;
using System.Runtime.InteropServices;
using xfa_formCS;
using XfaFlatteningApi.Contracts;
using Exception = System.Exception;

namespace XfaFlatteningApi.Utils
{
    public class FlattenerUtil : IFlattenerUtil
    {
        private readonly ILogger<FlattenerUtil> _logger;
        private readonly IConfiguration _config;

        public FlattenerUtil(IConfiguration config, ILogger<FlattenerUtil> logger)
        {
            _config = config;
            _logger = logger;
        }

        public async Task<byte[]> GetFlattenedBytesAsync(IFormFile? file)
        {
            var flattenedBytes = new byte[] { };
            var tempFolderPath = GetTempFolderPath();

            try
            {
                _logger.LogInformation("Creating temporary directory");
                Directory.CreateDirectory(tempFolderPath);

                var unFlattenedFilePath = await CreateUnFlattenedFileAsync(tempFolderPath, file);

                const string flattenedFileName = "flattened.pdf";
                var flattenedFilePath = System.IO.Path.Combine(tempFolderPath, flattenedFileName);

                flattenedBytes = FlattenTheFile(unFlattenedFilePath, flattenedFilePath);
            }
            catch (PDFException ex)
            {
                _logger.LogError(ex, "Error while flattening PDF file.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while flattening the PDF file.");
            }
            finally
            {
                Library.ReleaseOFDEngine();
                _logger.LogInformation("Foxit PDF SDK resources released.");
            }

            return flattenedBytes;
        }

        private string GetTempFolderPath()
        {
            var tempRootFolder = _config["TempFolderPath"];
            if (!Directory.Exists(tempRootFolder))
                throw new DirectoryNotFoundException("Either TempFolderPath directory is invalid or doesn't exists.");

            var uniqueId = Guid.NewGuid().ToString("N");

            var tempFolderName = $"temp-{uniqueId}";
            return System.IO.Path.Combine(tempRootFolder, tempFolderName);
        }

        private static async Task<string> CreateUnFlattenedFileAsync(string tempFolderPath, IFormFile? file)
        {
            const string unFlattenedFileName = "unflattened.pdf";
            var unFlattenedFilePath = System.IO.Path.Combine(tempFolderPath, unFlattenedFileName);

            var inputFileBytes = await FileUtil.GetFileBytes(file);
            await File.WriteAllBytesAsync(unFlattenedFilePath, inputFileBytes);

            return unFlattenedFilePath;
        }

        private byte[] FlattenTheFile(string unFlattenedFilePath, string flattenedFilePath)
        {
            _logger.LogInformation("Flattening the uploaded file.");

            using (var pdfDoc = new PDFDoc(unFlattenedFilePath))
            {
                var errorCode = pdfDoc.Load(null);
                if (errorCode != ErrorCode.e_ErrSuccess)
                    throw new Exception("Error while loading doc");

                //using var pXfaDocHandler = new CFS_XFADocHandler();
                using var xfaDoc = new XFADoc(pdfDoc);
                xfaDoc.StartLoad(null);
                xfaDoc.FlattenTo(flattenedFilePath);
            }

            _logger.LogInformation("Uploaded file flattened successfully!");

            return File.ReadAllBytes(flattenedFilePath);
        }

        public async Task<byte[]> GetFlattenedXfaPdfBytesAsync(IFormFile? file)
        {
            var flattenedBytes = new byte[] { };
            var tempFolderPath = GetTempFolderPath();

            try
            {
                //_foxitUtil.InitLibrary();

                _logger.LogInformation("Creating temporary directory");
                Directory.CreateDirectory(tempFolderPath);

                var unFlattenedFilePath = await CreateUnFlattenedFileAsync(tempFolderPath, file);

                const string flattenedFileName = "flattened.pdf";
                var flattenedFilePath = System.IO.Path.Combine(tempFolderPath, flattenedFileName);

                flattenedBytes = FlattenAllPagesInPdfFile(unFlattenedFilePath, flattenedFilePath);
            }
            catch (PDFException ex)
            {
                _logger.LogError(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                Library.Release();
            }

            return flattenedBytes;
        }

        private byte[] FlattenAllPagesInPdfFile(string unFlattenedFilePath, string flattenedFilePath)
        {
            _logger.LogInformation("Flattening the uploaded file.");

            using (var doc = new PDFDoc(unFlattenedFilePath))
            {
                var errorCode = doc.Load(null);
                if (errorCode != ErrorCode.e_ErrSuccess)
                    throw new Exception("Error while loading doc");

                // Get page count.
                var pageCount = doc.GetPageCount();

                // Flatten PDF pages.
                for (var i = 0; i < pageCount; i++)
                {
                    using (var page = doc.GetPage(i))
                    {
                        // Parse page before flattening the page.
                        page.StartParse((int)PDFPage.ParseFlags.e_ParsePageNormal, null, false);
                        page.Flatten(true, (int)PDFPage.FlattenOptions.e_FlattenAll);
                    }
                }

                doc.SaveAs(flattenedFilePath, (int)PDFDoc.SaveFlags.e_SaveFlagNormal);
                _logger.LogInformation("Uploaded file flattened successfully!");
            }

            return File.ReadAllBytes(flattenedFilePath);
        }

        public byte[]? GetFlattenedBytes(byte[] inputFileBytes)
        {
            try
            {
                var buffer = Marshal.AllocHGlobal(inputFileBytes.Length);
                Marshal.Copy(inputFileBytes, 0, buffer, inputFileBytes.Length);

                using (var doc = new PDFDoc(buffer, (uint)inputFileBytes.Length))
                {
                    var errorCode = doc.Load(null);
                    if (errorCode != ErrorCode.e_ErrSuccess)
                    {
                        throw new Exception("Error while loading doc");
                    }

                    var pXfaDocHandler = new CFS_XFADocHandler();
                    var streamCallback = new CustomStreamCallback();
                    using (var xfaDoc = new XFADoc(doc, pXfaDocHandler))
                    {
                        xfaDoc.StartLoad(null);
                        xfaDoc.FlattenTo(streamCallback);
                    }

                    streamCallback.Flush();
                    streamCallback.Release();

                    if (buffer != IntPtr.Zero)
                    {
                        Marshal.FreeHGlobal(buffer);
                    }

                    var flattenedBytes = streamCallback.GetFlattenedData();

                    return flattenedBytes;
                }
            }
            catch (PDFException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
