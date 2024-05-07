using System.Web;
using System.Web.Mvc;

namespace NguyenDucHuy_0158
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
