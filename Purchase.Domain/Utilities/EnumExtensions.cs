using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Purchase.Domain.Models;

namespace Purchase.Domain.Utilities
{
    public static class XpaEnumExtensions
    {
        public static List<LookupItem<int>> GetEnumList<T>()
        {

            return Enum.GetValues(typeof(T)).Cast<Enum>().Select(s => new LookupItem<int>(Convert.ToInt32(s), s.ToDescription(), Convert.ToInt32(s).ToString())).ToList();
        }

        public static string ToDescription(this Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            if (fi == null)
                return "0";


            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(
                typeof(DescriptionAttribute),
                false);

            if (attributes != null &&
                attributes.Length > 0)
                return attributes[0].Description.ToUpper();
            else
                return value.ToString();
        }
    }
}
