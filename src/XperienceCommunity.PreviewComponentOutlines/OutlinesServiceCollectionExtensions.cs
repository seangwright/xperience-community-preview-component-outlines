using Microsoft.AspNetCore.Razor.TagHelpers;
using XperienceCommunity.PreviewComponentOutlines;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class OutlinesServiceCollectionExtensions
    {
        /// <summary>
        /// Adds Page Builder component outlines and labels in Preview mode with no
        /// design customization
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddPreviewComponentOutlines(this IServiceCollection services)
        {
            return services.AddPreviewComponentOutlines(o => { });
        }

        /// <summary>
        /// Adds Page Builder component outlines and labels in Preview mode with design
        /// configuration provided by the <paramref name="configure" /> delegate
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configure"></param>
        /// <returns></returns>
        public static IServiceCollection AddPreviewComponentOutlines(this IServiceCollection services, Action<OutlinesConfiguration> configure)
        {
            return services
                .Configure(configure)
                .AddTransient<ITagHelperComponent, OutlinesStylesTagHelperComponent>();
        }
    }
}
