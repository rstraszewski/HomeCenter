using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HomeCenter.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString NgTemplateStart(this HtmlHelper htmlHelper, string Id)
        {
            var builder = new TagBuilder("script");
            builder.MergeAttribute("type", "text/ng-template");
            builder.MergeAttribute("id", Id);
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.StartTag));
        }

        public static MvcHtmlString NgTemplateEnd(this HtmlHelper htmlHelper)
        {
            var builder = new TagBuilder("script");
            return MvcHtmlString.Create(builder.ToString(TagRenderMode.EndTag));
        }

        public static AngularTemplateComponent NgTemplate(this HtmlHelper html, string Id)
        {
            html.ViewContext.Writer.Write(
                "<script type=\"text/ng-template\" id=\"" + Id + "\">"
                );
            return new AngularTemplateComponent(html.ViewContext);
        }

        public class AngularTemplateComponent : IDisposable
        {
            private readonly ViewContext _viewContext;
            public AngularTemplateComponent(ViewContext viewContext)
            {
                _viewContext = viewContext;
            }
            public void Dispose()
            {
                _viewContext.Writer.Write("</script>");
            }
        }

    }
}