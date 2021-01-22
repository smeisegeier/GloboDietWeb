using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace GloboDiet
{


    public static class Globals
    {
        //private static ProcessMilestone processMilestone;
        //public static List<int> GetListOfProcessMilestones()
        //{
        //    foreach (var item in (ProcessMilestone[])Enum.GetValues(myEnum.GetType()))
        //    {
        //        item.
        //    }
        //}

        /// <summary>
        /// Gets an object as array from delivered Enum. Hast to be caste
        /// </summary>
        /// <param name="myEnum"></param>
        /// <returns></returns>
        public static object GetArrayObjectFromEnum(Enum myEnum)
        {
            Type myType = myEnum.GetType();
            return Enum.GetValues(myType);
        }


            /// <summary>
            /// Extracts [DisplayName] from Enum Values
            /// https://stackoverflow.com/questions/13099834/how-to-get-the-display-name-attribute-of-an-enum-member-via-mvc-razor-code
            /// </summary>
            /// <param name="enumValue"></param>
            /// <returns></returns>
            public static string GetDisplayName(Enum enumValue)
            {
                return enumValue.GetType()
                                .GetMember(enumValue.ToString())
                                .First()
                                .GetCustomAttribute<DisplayAttribute>()
                                .GetName();
            }

            /// <summary>
            /// Gets an attribute on an enum field value
            /// </summary>
            /// <typeparam name="T">The type of the attribute you want to retrieve</typeparam>
            /// <param name="enumVal">The enum value</param>
            /// <returns>The attribute of type T that exists on the enum value</returns>
            /// <example><![CDATA[string desc = myEnumVariable.GetAttributeOfType<DescriptionAttribute>().Description;]]></example>
            public static T GetAttributeOfType<T>(this Enum enumVal) where T : System.Attribute
            {
                var type = enumVal.GetType();
                var memInfo = type.GetMember(enumVal.ToString());
                var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
                return (attributes.Length > 0) ? (T)attributes[0] : null;
            }

            /// <summary>
            /// Converts enum to a KeyValuePairList
            /// </summary>
            /// <remarks>
            /// https://stackoverflow.com/questions/36208281/enum-to-list-as-an-extension
            /// </remarks>
            /// <typeparam name="TEnum">Enum variable</typeparam>
            /// <returns>List of Pairs(EnumElement, Description)</returns>
            public static List<KeyValuePair<TEnum, string>> GetListWithDescription<TEnum>() where TEnum : struct
            {
                if (!typeof(TEnum).IsEnum) throw new InvalidOperationException();
                return ((TEnum[])Enum.GetValues(typeof(TEnum)))
                   .ToDictionary(k => k, v => ((Enum)(object)v).GetAttributeOfType<DescriptionAttribute>()?.Description)
                   .ToList();
            }

    }
}
