namespace XfaFlatteningApi.Contracts;

public interface IFoxitUtil
{
    /// <summary>
    /// Initialize the Foxit Library.
    /// 
    /// During the life-cycle of an application, this function can only be called once and should be called first before any other functions in Foxit PDF SDK can be called.
    /// Once function <b>Library.Release</b> is called, Foxit PDF SDK library cannot be initialized any more in the life-cycle of the application.
    /// For more information, visit <a href="https://developers.foxit.com/resources/pdf-sdk/dotnetcore_api_reference_windows/classfoxit_1_1common_1_1_library.html#a251b6859af5ae3dc95b87b4b74652c43">Foxit PDF SDK: Initialize()</a>.
    /// </summary>
    void InitLibrary();

    void ReInitLibrary();
    void SetLogFilePath();
}