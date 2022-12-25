namespace PaymentAPI.Extensions
{
    using Models;
    using System.ComponentModel;
    using System.Runtime.Serialization;

    public static class EnumSaleStatusExtension
    {
        public static string GetEnumMember(this EnumSaleStatus value)
        {
            var type = value.GetType();
            var name = Enum.GetName(type, value);

            if (name == null)
                return null;

            var field = type.GetField(name);

            if (field is null || !Attribute.IsDefined(field, typeof(EnumMemberAttribute)))
                return value.ToString();

            if (Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute)) is EnumMemberAttribute attr)
                return attr.Value;
            
            return null;
        }

        public static string GetEnumDescription(this EnumSaleStatus value)
        {
            var fi = value.GetType().GetField(value.ToString());

            if (fi == null) return value.GetEnumMember();

            if (fi.GetCustomAttributes(typeof(DescriptionAttribute), false) is DescriptionAttribute[] attributes && attributes.Any())
                return attributes.First().Description;

            return value.GetEnumMember();
        }
    }
}
