using Microsoft.AspNetCore.Http;
using Kentico.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Kentico.Content.Web.Mvc;

namespace XperienceCommunity.PreviewComponentOutlines;

[HtmlTargetElement("*", Attributes = TAG_HELPER_ATTRIBUTE)]
public class OutlineTagHelper : TagHelper
{
    public const string TAG_HELPER_ATTRIBUTE = "xpc-preview-outline";

    private readonly IHttpContextAccessor accessor;

    [HtmlAttributeName(TAG_HELPER_ATTRIBUTE)]
    public string? ComponentName { get; set; }

    public OutlineTagHelper(IHttpContextAccessor accessor)
    {
        this.accessor = accessor;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Remove(new(TAG_HELPER_ATTRIBUTE));

        var httpContext = accessor.HttpContext;

        bool isPreviewMode = !httpContext.Kentico().PageBuilder().EditMode && httpContext.Kentico().Preview().Enabled;

        if (!isPreviewMode || ComponentName is null)
        {
            return;
        }

        output.Attributes.Add("data-xpc-preview-outline", ComponentName);
    }
}
