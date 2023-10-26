using System.ComponentModel;
using System.Globalization;

namespace Helpers.ObjectsUtils.Extension
{
    public static class EnumeratorExtensions
    {
        public static string GetDescription<T>(this T enumeration) where T : IConvertible
        {
            if (enumeration is Enum)
            {
                Type type = enumeration.GetType();
                Array values = Enum.GetValues(type);

                foreach (int value in values)
                {
                    if (value == enumeration.ToInt32(CultureInfo.InvariantCulture))
                    {
                        System.Reflection.MemberInfo[] publicMemberForValue = type.GetMember(type.GetEnumName(value));

                        if (publicMemberForValue[0]
                            .GetCustomAttributes(typeof(DescriptionAttribute), false)
                            .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                        {
                            return descriptionAttribute.Description;
                        }
                    }
                }
            }
            return string.Empty;
        }
    }
}
