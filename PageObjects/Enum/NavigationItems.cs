using WebDriverFramework.Util;

namespace PageObjects.Enum
{
    //public enum ElementsNavigationItems
    //{
    //    [EnumExtensions.StringMapping("Text Box")]
    //    TextBox,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    CheckBox,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    RadioButton,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    WebTables,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    Buttons,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    Links,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    BrokenLinks,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    Upload,

    //    [EnumExtensions.StringMapping("Text Box")]
    //    DynamicProperties
    //}

    //public enum FormsNavigationItems
    //{
    //    PracticeForm
    //}

    public enum NavigationItems
    {

        [EnumExtensions.StringMapping("Links")]
        Links,

        [EnumExtensions.StringMapping("Browser Windows")]
        BrowserWindows,

        [EnumExtensions.StringMapping("Alerts")]
        Alerts,

        [EnumExtensions.StringMapping("Frames")]
        Frames,

        [EnumExtensions.StringMapping("Nested Frames")]
        NestedFrames,

        [EnumExtensions.StringMapping("Modal Dialogs")]
        ModalDialogs,
        
        [EnumExtensions.StringMapping("Text Box")]
        TextBox,

        [EnumExtensions.StringMapping("Check Box")]
        CheckBox,

        [EnumExtensions.StringMapping("Radio Button")]
        RadioButton,

        [EnumExtensions.StringMapping("Web Tables")]
        WebTables,

        [EnumExtensions.StringMapping("Buttons")]
        Buttons,

        [EnumExtensions.StringMapping("Progress Bar")]
        ProgressBar,

        [EnumExtensions.StringMapping("Slider")]
        Slider,

        [EnumExtensions.StringMapping("Date Picker")]
        DatePicker,

        [EnumExtensions.StringMapping("Upload and Download")]
        UploadAndDownload,
    }

}
