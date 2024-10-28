using Microsoft.AspNetCore.Http;
using Kentico.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Kentico.Content.Web.Mvc;

namespace XperienceCommunity.PreviewComponentOutlines;

/// <summary>
/// Decorates an HTML element with a custom attribute used for outline and label styles
/// in Page Builder Preview mode
/// </summary>
[HtmlTargetElement("*", Attributes = TAG_HELPER_ATTRIBUTE)]
public class OutlineTagHelper(IHttpContextAccessor accessor, IPageBuilderDataContextRetriever contextRetriever) : TagHelper
{
    public const string TAG_HELPER_ATTRIBUTE = "xpc-preview-outline";
    public const string TAG_HELPER_OUTPUT_ATTRIBUTE = "data-xpc-preview-outline";
    public const string TAG_HELPER_REMOVE_ELEMENT_ATTRIBUTE = "xpc-preview-outline-remove-element";

    private readonly IHttpContextAccessor accessor = accessor;
    private readonly IPageBuilderDataContextRetriever contextRetriever = contextRetriever;

    /// <summary>
    /// The name of the component that will be displayed in the outline. Must end in 'Section' or 'Widget'.
    /// </summary>
    [HtmlAttributeName(TAG_HELPER_ATTRIBUTE)]
    public string? ComponentName { get; set; }

    /// <summary>
    /// Indicates if the element with the <see cref="TAG_HELPER_ATTRIBUTE" /> attribute should be removed
    /// when rendered in Live mode.
    /// </summary>
    [HtmlAttributeName(TAG_HELPER_REMOVE_ELEMENT_ATTRIBUTE)]
    public bool? RemoveElement { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.Attributes.Remove(new(TAG_HELPER_ATTRIBUTE));

        var httpContext = accessor.HttpContext;

        bool isPreviewMode = contextRetriever.Retrieve().GetMode() == PageBuilderMode.Off
            && httpContext.Kentico().Preview().Enabled;

        if (!isPreviewMode || ComponentName is null)
        {
            if (RemoveElement is bool removeElement && removeElement)
            {
                output.TagName = null;
            }

            return;
        }

        output.Attributes.Add(TAG_HELPER_OUTPUT_ATTRIBUTE, ComponentName);
    }
}
