using FluentAssertions;
using Kentico.PageBuilder.Web.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace XperienceCommunity.PreviewComponentOutlines.Tests;

public class OutlineTagHelperTests
{
    [Test]
    public void The_Data_Attribute_Will_Not_Be_Added_When_The_Request_Is_In_Live_Mode()
    {
        string componentName = "My Test Widget";

        var accessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = Substitute.For<HttpContext>();
        httpContext.Items = new Dictionary<object, object?>
        {
            { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: false, isPreviewModeEnabled: false) }
        };
        accessor.HttpContext.Returns(httpContext);
        var contextRetriever = Substitute.For<IPageBuilderDataContextRetriever>();
        var dataContext = Substitute.For<IPageBuilderDataContext>();
        dataContext.EditMode.Returns(false);
        contextRetriever.Retrieve().Returns(dataContext);

        var tagHelperContext = new TagHelperContext("section", [], new Dictionary<object, object>(), Guid.NewGuid().ToString());
        var output = new TagHelperOutput("section", [], (val, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        var sut = new OutlineTagHelper(accessor, contextRetriever)
        {
            ComponentName = componentName
        };
        sut.Process(tagHelperContext, output);

        output.Attributes
            .Where(a => string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_ATTRIBUTE, StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();

        output.Attributes
            .Where(a =>
                string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_OUTPUT_ATTRIBUTE, StringComparison.OrdinalIgnoreCase)
                && string.Equals(a.Value.ToString(), componentName, StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void The_Data_Attribute_Will_Not_Be_Added_When_The_Request_Is_In_PageBuilder_Mode()
    {
        string componentName = "My Test Widget";

        var accessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = Substitute.For<HttpContext>();
        httpContext.Items = new Dictionary<object, object?>
        {
            { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: true, isPreviewModeEnabled: false) }
        };
        accessor.HttpContext.Returns(httpContext);
        var contextRetriever = Substitute.For<IPageBuilderDataContextRetriever>();
        var dataContext = Substitute.For<IPageBuilderDataContext>();
        dataContext.EditMode.Returns(true);
        contextRetriever.Retrieve().Returns(dataContext);

        var tagHelperContext = new TagHelperContext("section", [], new Dictionary<object, object>(), Guid.NewGuid().ToString());
        var output = new TagHelperOutput("section", [], (val, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        var sut = new OutlineTagHelper(accessor, contextRetriever)
        {
            ComponentName = componentName
        };
        sut.Process(tagHelperContext, output);

        output.Attributes
            .Where(a => string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_ATTRIBUTE, StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();

        output.Attributes
            .Where(a =>
                string.Equals(a.Name, OutlineTagHelper.TAG_HELPER_OUTPUT_ATTRIBUTE, StringComparison.OrdinalIgnoreCase)
                && string.Equals(a.Value.ToString(), componentName, StringComparison.OrdinalIgnoreCase))
            .Should().BeEmpty();
    }

    [Test]
    public void The_Data_Attribute_Will_Be_Added_When_The_Request_Is_In_Preview_Mode()
    {
        string componentName = "My Test Widget";

        var accessor = Substitute.For<IHttpContextAccessor>();
        var httpContext = Substitute.For<HttpContext>();
        httpContext.Items = new Dictionary<object, object?>
        {
            { "Kentico.Features", ContextFixtures.InitializeFeatures(isEditModeEnabled: false, isPreviewModeEnabled: true) }
        };
        accessor.HttpContext.Returns(httpContext);
        var contextRetriever = Substitute.For<IPageBuilderDataContextRetriever>();
        var dataContext = Substitute.For<IPageBuilderDataContext>();
        dataContext.EditMode.Returns(false);
        contextRetriever.Retrieve().Returns(dataContext);

        var tagHelperContext = new TagHelperContext("section", [], new Dictionary<object, object>(), Guid.NewGuid().ToString());
        var output = new TagHelperOutput("section", [], (val, encoder) => Task.FromResult<TagHelperContent>(new DefaultTagHelperContent()));

        var sut = new OutlineTagHelper(accessor, contextRetriever)
        {
            ComponentName = componentName
        };
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
}
