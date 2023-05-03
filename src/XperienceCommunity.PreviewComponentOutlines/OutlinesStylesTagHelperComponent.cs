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
                return @"<style>
[data-page-builder-component]:hover {
    outline: 2px #ccc dashed;
    position: relative;
}

[data-page-builder-component$='Section']:hover::before {
    top: 0;
    left: 50%;
    transform: translate(-50%, 0);
    content: attr(data-page-builder-component);
    white-space: pre;
    display: inline;
    font-size: 1rem;
    position: absolute;
}

[data-page-builder-component$='Widget']:hover::before {
    top: 0;
    left: 0;
    left: 0.2rem;
    content: attr(data-page-builder-component);
    white-space: pre;
    display: inline;
    font-size: 1rem;
    position: absolute;
}
</style>";
            }
        }

        private readonly IHttpContextAccessor accessor;
        private readonly OutlinesConfiguration config;

        public override int Order => 1;

        public OutlinesStylesTagHelperComponent(IHttpContextAccessor accessor, IOptions<OutlinesConfiguration> options)
        {
            this.accessor = accessor;
            config = options.Value;
        }

        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var httpContext = accessor.HttpContext;

            bool isPreviewMode = !httpContext.Kentico().PageBuilder().EditMode && httpContext.Kentico().Preview().Enabled;

            if (!isPreviewMode)
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
