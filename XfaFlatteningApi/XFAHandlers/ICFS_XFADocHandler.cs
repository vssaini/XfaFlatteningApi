using foxit.addon.xfa;
using foxit.common.fxcrt;

namespace xfa_formCS
{
    internal interface ICFS_XFADocHandler
    {
        void DisplayCaret(int page_index, bool is_visible, RectF rect);
        void Dispose();
        void ExportData(XFADoc doc, string file_path);
        int GetCurrentPage(XFADoc doc);
        uint GetHighlightColor(XFADoc doc);
        bool GetPopupPos(int page_index, float min_popup, float max_popup, RectF rect_widget, RectF inout_rect_popup);
        string GetTitle(XFADoc doc);
        void GotoURL(XFADoc doc, string url);
        void ImportData(XFADoc doc, string file_path);
        void InvalidateRect(int page_index, RectF rect, DocProviderCallback.InvalidateFlag flag);
        void PageViewEvent(int page_index, DocProviderCallback.PageViewEventType page_view_event_type);
        bool PopupMenu(int page_index, PointF rect_popup);
        void Print(XFADoc doc, int start_page_index, int end_page_index, int options);
        void Release();
        void SetChangeMark(XFADoc doc);
        void SetCurrentPage(XFADoc doc, int current_page_index);
        void SetFocus(XFAWidget xfa_widget);
        bool SubmitData(XFADoc doc, string target, DocProviderCallback.SubmitFormat format, DocProviderCallback.TextEncoding text_encoding, string content);
        void WidgetEvent(XFAWidget xfa_widget, DocProviderCallback.WidgetEventType widget_event_type);
    }
}