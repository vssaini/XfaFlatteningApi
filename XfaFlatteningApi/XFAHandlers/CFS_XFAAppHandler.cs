using foxit.common;
using foxit.common.fxcrt;
using foxit.addon.xfa;

namespace xfa_formCS
{
    class CFS_XFAAppHandler : AppProviderCallback
    {
        public CFS_XFAAppHandler()
        {
        }
        public override void Beep(AppProviderCallback.BeepType type)
        {

        }
        public override void Dispose()
        {

        }
        public override FileReaderCallback? DownLoadUrl(string url)
        {
            return null;
        }
        public override string GetAppInfo(AppProviderCallback.AppInfo app_info)
        {
            return "Foxit SDK";
        }
        public override string LoadString(AppProviderCallback.StringID string_id)
        {
            return "LoadString";
        }
        public override AppProviderCallback.MsgBoxButtonID MsgBox(string message, string title, AppProviderCallback.MsgBoxIconType icon_type, AppProviderCallback.MsgBoxButtonType button_type)
        {
            return AppProviderCallback.MsgBoxButtonID.e_MsgBtnIDYes;
        }
        public override string PostRequestURL(string url, string data, string content_type, string encode, string header)
        {
            return "PostRequestUrl";
        }
        public override bool PutRequestURL(string url, string data, string encode)
        {
            return true;
        }
        public override void Release()
        {
        }
        public override string Response(string question, string title, string default_answer, bool is_mask)
        {
            return "answer";
        }
        public override WStringArray ShowFileDialog(string string_title, string string_filter, bool is_openfile_dialog)
        {
            return new WStringArray();
        }
    }
}
