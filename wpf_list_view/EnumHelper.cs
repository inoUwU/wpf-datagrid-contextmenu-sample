using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace wpf_list_view
{
    public static class EnumHelper
    {
        public static IEnumerable<string> GetEnumDisplayNames<T>() where T : Enum
        {
            return typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static)
                            .Select(f => f.GetCustomAttributes(typeof(DisplayAttribute), false)
                                          .Cast<DisplayAttribute>()
                                          .FirstOrDefault()?.Name ?? f.Name);
        }
    }
}
