namespace XperienceCommunity.PreviewComponentOutlines;

public class OutlinesConfiguration
{
    /// <summary>
    /// Color of the outline surrounding Page Builder components
    /// </summary>
    /// <value></value>
    public string OutlineColor { get; set; } = "#ccc";
    /// <summary>
    /// Font size of the label text for each Page Builder component
    /// </summary>
    /// <value></value>
    public string LabelFontSize { get; set; } = "1rem";
    /// <summary>
    /// Font color of the label text for each Page Builder component
    /// </summary>
    /// <value></value>
    public string LabelFontColor { get; set; } = "#7f09b7";
    /// <summary>
    /// Background color of the label for each Page Builder component
    /// </summary>
    /// <value></value>
    public string LabelBackgroundColor { get; set; } = "#fff";
    /// <summary>
    /// Border color of the label for each Page Builder component
    /// </summary>
    /// <value></value>
    public string LabelBorderColor { get; set; } = "#7f09b7";
    /// <summary>
    /// Padding of the label for each Page Builder component
    /// </summary>
    /// <value></value>
    public string LabelPadding { get; set; } = ".5rem";
    /// <summary>
    /// Opacity of the label for each Page Builder Component
    /// </summary>
    /// <value></value>
    public string LabelOpacity { get; set; } = ".8";
    /// <summary>
    /// If true, the styles provided by the library will be used for Page Builder component
    /// outlines and labels along with any customized values in this configuration class.
    /// 
    /// If false, the user will be expected to provide their own styles.
    /// </summary>
    /// <value>Defaults to true</value>
    public bool UseIncludedStyles { get; set; } = true;
}
