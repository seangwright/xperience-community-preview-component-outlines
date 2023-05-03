using Microsoft.AspNetCore.Razor.TagHelpers;
using XperienceCommunity.PreviewComponentOutlines;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OutlinesServiceCollectionExtensions
    {
        public static IServiceCollection AddPreviewComponentOutlines(this IServiceCollection services)
        {
            return services.AddPreviewComponentOutlines(o => { });
        }

        public static IServiceCollection AddPreviewComponentOutlines(this IServiceCollection services, Action<OutlinesConfiguration> configure)
        {
            return services
                .Configure(configure)
                .AddTransient<ITagHelperComponent, OutlinesStylesTagHelperComponent>();
        }
    }
}
