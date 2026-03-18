using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace RestWithAspNet10.Hypermedia.Utils
{
    public static class UrlHelper
    {
        private static readonly object _lock = new();
        public static string BuildBaseUrl(this IUrlHelper urlHelper, string routeName, string path)
        {
            lock (_lock)
            {
                var url = urlHelper.Link(routeName, new { controller = path}) ?? string.Empty;
                //localhost:5000/api/person/v1
                //localhost:5000%2F/api%2F/person%2F/v1
                return url.Replace("%2F","/").TrimEnd('/');
            }
        }
    }
}
