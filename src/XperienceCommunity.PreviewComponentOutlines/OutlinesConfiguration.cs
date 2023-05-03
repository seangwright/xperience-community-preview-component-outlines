namespace XperienceCommunity.PreviewComponentOutlines
{
    public class OutlinesConfiguration
    {
        public string OutlineColor { get; set; } = "#ccc";
        public string FontSize { get; set; } = "1rem";
        public string FontColor { get; set; } = "#7f09b7";
        public string BackgroundColor { get; set; } = "#fff";
        public string LabelBorderColor { get; set; } = "#7f09b7";
        public string Padding { get; set; } = ".5rem";
        public string Opacity { get; set; } = ".8";
        /// <summary>
        /// If true, the styles provided by the library will be used for Page Builder component
        /// outlines and labels along with any customized values in this configuration class.
        /// 
        /// If false, the user will be expected to provide their own styles.
        /// </summary>
        /// <value>Defaults to true</value>
        public bool UseIncludedStyles { get; set; } = true;
    }
}
