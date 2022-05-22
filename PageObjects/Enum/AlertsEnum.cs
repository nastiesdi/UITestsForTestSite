using WebDriverFramework.Util;

namespace PageObjects.Enum
{
    public enum AlertsEnum
    {
        [EnumExtensions.Description("You clicked a button")]
        alertButton,

        [EnumExtensions.Description("This alert appeared after 5 seconds")]
        timerAlertButton,

        [EnumExtensions.Description("Do you confirm action?")]
        confirmButton,

        [EnumExtensions.Description("Please enter your name")]
        promtButton
    }

    public enum AlertResultEnum
    {
        [EnumExtensions.Description("You selected Ok")]
        OkSelected,

        [EnumExtensions.Description("You selected Cancel")]
        cancelSelected,
    }
}
