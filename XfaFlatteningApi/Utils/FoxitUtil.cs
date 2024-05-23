using foxit.common;
using XfaFlatteningApi.Contracts;

namespace XfaFlatteningApi.Utils;

public class FoxitUtil : IFoxitUtil
{
    private readonly IConfiguration _config;
    private readonly ILogger<FoxitUtil> _logger;

    public FoxitUtil(IConfiguration config, ILogger<FoxitUtil> logger)
    {
        _config = config;
        _logger = logger;
    }

    public void InitLibrary()
    {
        _logger.LogInformation("Initializing the Foxit Library...");

        var sn = _config["FoxitLicensing:Sn"];
        var key = _config["FoxitLicensing:Key"];

        var errorCode = Library.Initialize(sn, key, is_optimize_memory: true);
        if (errorCode != ErrorCode.e_ErrSuccess)
        {
            _logger.LogError("Library Initialize Error: {ErrorCode}", errorCode);
            Library.Release();
        }
    }

    public void ReInitLibrary()
    {
        _logger.LogInformation("Re initializing the Foxit Library...");

        var errorCode = Library.Reinitialize();
        if (errorCode != ErrorCode.e_ErrSuccess)
        {
            _logger.LogError("Library ReInitialize Error: {ErrorCode}", errorCode);
            Library.Release();
        }
    }

    public void SetLogFilePath()
    {
        _logger.LogInformation("Setting the log file path...");
        
        const string logFilePath = "E:\\repos\\FlattenerManagement\\Logs.txt";
        Library.SetLogFile(logFilePath);
    }
}