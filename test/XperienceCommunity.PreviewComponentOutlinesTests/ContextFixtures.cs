using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;

namespace XperienceCommunity.PreviewComponentOutlinesTests
{
    public static class ContextFixtures
    {
        public static IFeatureSet InitializeFeatures(bool isEditModeEnabled, bool isPreviewModeEnabled)
        {
            var previewFeature = Substitute.For<IPreviewFeature>();
            previewFeature.Enabled.Returns(isPreviewModeEnabled);
            var features = Substitute.For<IFeatureSet>();
            features.GetFeature<IPreviewFeature>().Returns(previewFeature);
            var pageBuilderFeature = Substitute.For<IPageBuilderFeature>();
            pageBuilderFeature.EditMode.Returns(isEditModeEnabled);
            features.GetFeature<IPageBuilderFeature>().Returns(pageBuilderFeature);

            return features;
        }
    }
}
