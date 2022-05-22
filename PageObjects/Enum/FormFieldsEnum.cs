using WebDriverFramework.Util;

namespace PageObjects.Enum
{
    public enum FormFieldsEnum
    {
        [EnumExtensions.StringMapping("First Name")]
        firstName,

        [EnumExtensions.StringMapping("Last Name")]
        lastName,

        [EnumExtensions.StringMapping("Email")]
        userEmail,

        [EnumExtensions.StringMapping("Age")]
        age,

        [EnumExtensions.StringMapping("Salary")]
        salary,

        [EnumExtensions.StringMapping("Department")]
        department
    }
}
