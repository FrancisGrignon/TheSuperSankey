using Sankey.Domain.Models;

namespace Sankey.API
{
    public static class ICodesetExtensions
    {
        public static string Name(this ICodeset value, string language)
        {
            return language.Equals("fr") ? value.NameFr : value.NameEn;
        }
    }
}
