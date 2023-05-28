using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using XperienceCommunity.PreviewComponentOutlines;

namespace XperienceCommunity.PreviewComponentOutlinesTests
{
    public class OutlinesStylesTagHelperComponentTests
    {
        [Test]
        public async Task The_Styles_Element_Will_Not_Be_Added_When_The_Request_Is_In_Live_Mode()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContext.Items = new Dictionary<object, object?>
            {
                { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: false, isPreviewModeEnabled: false) }
            };
            accessor.HttpContext.Returns(httpContext);

            var tagHelperContext = new TagHelperContext("head", new(), new Dictionary<object, object>(), Guid.NewGuid().ToString());
            var output = new TagHelperOutput("head", new(), (val, encoder) =>
            {
                return Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
            });

            var options = Options.Create(new OutlinesConfiguration());

            var sut = new OutlinesStylesTagHelperComponent(accessor, options);

            await sut.ProcessAsync(tagHelperContext, output);

            output.PostContent.IsEmptyOrWhiteSpace.Should().BeTrue();
        }

        [Test]
        public async Task The_Styles_Element_Will_Not_Be_Added_When_The_Request_Is_In_Edit_Mode()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContext.Items = new Dictionary<object, object?>
            {
                { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: true, isPreviewModeEnabled: false) }
            };
            accessor.HttpContext.Returns(httpContext);

            var tagHelperContext = new TagHelperContext("head", new(), new Dictionary<object, object>(), Guid.NewGuid().ToString());
            var output = new TagHelperOutput("head", new(), (val, encoder) =>
            {
                return Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
            });

            var options = Options.Create(new OutlinesConfiguration());

            var sut = new OutlinesStylesTagHelperComponent(accessor, options);

            await sut.ProcessAsync(tagHelperContext, output);

            output.PostContent.IsEmptyOrWhiteSpace.Should().BeTrue();
        }

        [Test]
        public async Task The_Styles_Element_Will_Not_Be_Added_When_The_Request_Is_In_Preview_Mode_And_The_Configuration_Does_Not_Use_Included_Styles()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContext.Items = new Dictionary<object, object?>
            {
                { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: false, isPreviewModeEnabled: true) }
            };
            accessor.HttpContext.Returns(httpContext);

            var tagHelperContext = new TagHelperContext("head", new(), new Dictionary<object, object>(), Guid.NewGuid().ToString());
            var output = new TagHelperOutput("head", new(), (val, encoder) =>
            {
                return Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
            });

            var options = Options.Create(new OutlinesConfiguration
            {
                UseIncludedStyles = false
            });

            var sut = new OutlinesStylesTagHelperComponent(accessor, options);

            await sut.ProcessAsync(tagHelperContext, output);

            output.PostContent.IsEmptyOrWhiteSpace.Should().BeTrue();
        }

        [Test]
        public async Task The_Styles_Element_Will_Be_Added_When_The_Request_Is_In_Preview_Mode()
        {
            var accessor = Substitute.For<IHttpContextAccessor>();
            var httpContext = Substitute.For<HttpContext>();
            httpContext.Items = new Dictionary<object, object?>
            {
                { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: false, isPreviewModeEnabled: true) }
            };
            accessor.HttpContext.Returns(httpContext);

            var tagHelperContext = new TagHelperContext("head", new(), new Dictionary<object, object>(), Guid.NewGuid().ToString());
            var output = new TagHelperOutput("head", new(), (val, encoder) =>
            {
                return Task.FromResult<TagHelperContent>(new DefaultTagHelperContent());
            });

            var config = new OutlinesConfiguration();
            var options = Options.Create(config);

            var sut = new OutlinesStylesTagHelperComponent(accessor, options);

            await sut.ProcessAsync(tagHelperContext, output);

            var content = output.PostContent.GetContent();

            content.Should().BeEquivalentTo(sut.Style);
        }
    }
}
