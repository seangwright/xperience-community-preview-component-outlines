using Microsoft.Extensions.DependencyInjection;

namespace XperienceCommunity.PreviewComponentOutlines
{
    public static class OutlinesServiceCollectionExtensions
    {
        public static IServiceCollection AddPreviewComponentOutlines(this IServiceCollection services, Action<OutlinesConfiguration> configure)
        {
            return services.Configure(configure);
        }
    }
}
