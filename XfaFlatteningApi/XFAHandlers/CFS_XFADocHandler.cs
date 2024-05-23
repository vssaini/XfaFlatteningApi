using foxit.common.fxcrt;
using foxit.addon.xfa;

namespace xfa_formCS
{
    class CFS_XFADocHandler : DocProviderCallback
    {
        public CFS_XFADocHandler()
        {

        }

        public override void DisplayCaret(int page_index, bool is_visible, RectF rect)
        {

        }
        public override void Dispose()
        {

        }
        public override void ExportData(XFADoc doc, string file_path)
        {

        }
        public override int GetCurrentPage(XFADoc doc)
        {
            return 0;
        }
        public override uint GetHighlightColor(XFADoc doc)
        {
            if (doc.GetType() == XFADoc.Type.e_Static)
            {
                return 0x50FF0000;
            }
            else
            {
                return 0x500000FF;
            }
        }
        public override bool GetPopupPos(int page_index, float min_popup, float max_popup, RectF rect_widget, RectF inout_rect_popup)
        {
            return true;
        }
        public override string GetTitle(XFADoc doc)
        {
            return "title";
        }
        public override void GotoURL(XFADoc doc, string url)
        {

        }
        public override void ImportData(XFADoc doc, string file_path)
        {

        }
        public override void InvalidateRect(int page_index, RectF rect, DocProviderCallback.InvalidateFlag flag)
        {

        }
        public override void PageViewEvent(int page_index, DocProviderCallback.PageViewEventType page_view_event_type)
        {

        }
        public override bool PopupMenu(int page_index, foxit.common.fxcrt.PointF rect_popup)
        {
            return true;
        }
        public override void Print(XFADoc doc, int start_page_index, int end_page_index, int options)
        {

        }
        public override void Release()
        {

        }
        public override void SetChangeMark(XFADoc doc)
        {

        }
        public override void SetCurrentPage(XFADoc doc, int current_page_index)
        {

        }
        public override void SetFocus(XFAWidget xfa_widget)
        {

        }
        public override bool SubmitData(XFADoc doc, string target, DocProviderCallback.SubmitFormat format, DocProviderCallback.TextEncoding text_encoding, string content)
        {
            return true;
        }
        public override void WidgetEvent(XFAWidget xfa_widget, DocProviderCallback.WidgetEventType widget_event_type)
        {

        }
    }
}
