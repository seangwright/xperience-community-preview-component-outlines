using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace XperienceCommunity.PreviewComponentOutlines
{
    public class OutlinesStylesTagHelperComponent : TagHelperComponent
    {
        private string Style
        {
            get
            {
                return $$"""
<style>
[data-xpc-preview-outline]:hover {
    outline: 2px {{config.OutlineColor}} dashed;
    position: relative;
}

[data-xpc-preview-outline]:hover::before {
    display: inline;
    position: absolute;
    white-space: pre;
    content: attr(data-xpc-preview-outline);
    padding: {{config.Padding}};
    color: {{config.FontColor}};
    background-color: {{config.BackgroundColor}};
    border: 1px solid {{config.LabelBorderColor}};
    border-radius: 5000px;
    font-size: {{config.FontSize}};
    z-index: 1;
    opacity: {{config.Opacity}};
    isolation: isolate;
}

[data-xpc-preview-outline$='Section']:hover::before {
    top: .2rem;
    left: 50%;
    transform: translate(-50%, 0);
}

[data-xpc-preview-outline$='Widget']:hover::before {
    top: .2rem;
    left: 0;
    left: 0.2rem;
}
</style>
""";
            }
        }

        private readonly IHttpContextAccessor accessor;
        private readonly OutlinesConfiguration config;

        public override int Order => 100;

        public OutlinesStylesTagHelperComponent(IHttpContextAccessor accessor, IOptions<OutlinesConfiguration> options)
        {
            this.accessor = accessor;
            config = options.Value;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var httpContext = accessor.HttpContext;

            bool isPreviewMode = !httpContext.Kentico().PageBuilder().EditMode && httpContext.Kentico().Preview().Enabled;

            if (!isPreviewMode || !config.UseIncludedStyles)
            {
                return Task.CompletedTask;
            }

            if (string.Equals(context.TagName, "head", StringComparison.OrdinalIgnoreCase))
            {
                output.PostContent.AppendHtml(Style);
            }

            return Task.CompletedTask;
        }
    }
}
