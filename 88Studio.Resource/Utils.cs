using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BGP.Utils.Common;

namespace _88Studio.Resource
{
    public static class Utils
    {
        /// <summary>
        /// Add prefix by the input type
        /// </summary>
        /// <typeparam name="TObjectType"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string Prefix<TObjectType>(this string key)
        {
            return typeof(TObjectType).Name + "_" + key;
        }

        public static string GetEnumDisplayText(this Enum enumVal)
        {
            string displayText = enumVal.ToString();

            var enumInfo = enumVal.GetEnumAttribute<EnumInformationAttribute>();

            if (enumInfo != null)
            {
                if (enumInfo.ResourceType != null && !string.IsNullOrEmpty(enumInfo.ResourceName))
                {
                    var property = enumInfo.ResourceType.GetProperty(enumInfo.ResourceName);

                    if (property == null)
                    {
                        throw new Exception(string.Format("Resource name \"{0}\" is not found in type \"{1}\"", enumInfo.ResourceName, enumInfo.ResourceType.FullName));
                    }

                    displayText = property.GetValue(null).ToString();
                }
                else if (!string.IsNullOrEmpty(enumInfo.Description))
                {
                    displayText = enumInfo.Description;
                }
            }

            return displayText;
        }


        public static TEnum ToEnum<TEnum>(this string str, bool useResource = false)
        {
            if (useResource)
            {
                foreach (var enumValue in Enum.GetValues(typeof(TEnum)).OfType<TEnum>())
                {
                    var text = (enumValue as System.Enum).GetEnumDisplayText();
                    if (text.Equals(str, StringComparison.CurrentCultureIgnoreCase))
                    {
                        return enumValue;
                    }
                }
            }

            return (TEnum)Enum.Parse(typeof(TEnum), str);
        }
    }
}
