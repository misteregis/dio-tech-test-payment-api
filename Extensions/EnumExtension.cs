using System.Runtime.Serialization;

namespace PaymentAPI.Extensions;

public static class Extension
{
    public static string GetEnumMemberValue(this Enum @enum)
    {
        var attr = @enum.GetType().GetMember(@enum.ToString()).FirstOrDefault()?.
                GetCustomAttributes(false).OfType<EnumMemberAttribute>().
                FirstOrDefault();

        return attr == null ? @enum.ToString() : attr.Value;
    }
}