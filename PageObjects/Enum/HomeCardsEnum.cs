using WebDriverFramework.Util;

namespace PageObjects
{
    public enum HomeCardsEnum
    {
        [EnumExtensions.StringMapping("Elements")]
        Elements,

        [EnumExtensions.StringMapping("Forms")]
        Forms,

        [EnumExtensions.StringMapping("Alerts, Frame & Windows")]
        Alerts,

        [EnumExtensions.StringMapping("Widgets")]
        Widgets,

        [EnumExtensions.StringMapping("Interactions")]
        Interactions,

        [EnumExtensions.StringMapping("Book Store Application")]
        Book
    }
}
