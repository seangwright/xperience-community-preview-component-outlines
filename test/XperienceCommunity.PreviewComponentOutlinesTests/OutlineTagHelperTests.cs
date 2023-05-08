using FluentAssertions;
using Kentico.Content.Web.Mvc;
using Kentico.PageBuilder.Web.Mvc;
using Kentico.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NSubstitute;

namespace XperienceCommunity.PreviewComponentOutlines;

public class OutlineTagHelperTests
{

    [Test]
    public void The_Data_Attribute_Will_Be_Added_When_The_Request_Is_In_Preview_Mode()
    {
        string componentName = "My Test Widget";

        var accessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = Substitute.For<HttpContext>();
        httpContext.Items = new Dictionary<object, object?>
        {
            { "Kentico.Features", InitializeFeatures(false, true) }
        };
        accessor.HttpContext.Returns(httpContext);

        var tagHelperContext = new TagHelperContext("section", new(), new Dictionary<object, object>(), Guid.NewGuid().ToString());
        var output = new TagHelperOutput("section", new(), (val, encoder) =>
        {
            return Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
        });

        var sut = new OutlineTagHelper(accessor);

        sut.ComponentName = componentName;
        sut.Process(tagHelperContext, output);

        output.Attributes
            .Where(a => string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_ATTRIBUTE, StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();

        output.Attributes
            .Where(a =>
                string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_OUTPUT_ATTRIBUTE, StringComparison.OrdinalIgnoreCase)
                && string.Equals(a.Value.ToString(), componentName, StringComparison.OrdinalIgnoreCase))
            .Should().HaveCount(1);
    }

    private IFeatureSet InitializeFeatures(bool isEditModeEnabled, bool isPreviewModeEnabled)
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
