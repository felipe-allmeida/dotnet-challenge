using System.Collections;
using System.Web;

namespace BikeRental.API.FunctionalTests.Extensions
{
    public static class ObjectExtensions
    {
        public static string GetQueryString(this object obj)
        {
            //var properties = from p in obj.GetType().GetProperties()
            //                 where p.GetValue(obj, null) != null
            //                 select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            var props = new List<string>();
            foreach (var p in obj.GetType().GetProperties())
            {
                var propValue = p.GetValue(obj, null);
                if (propValue == null) continue;

                switch (propValue)
                {
                    case string[]:
                    case int[]:
                        foreach (var item in (IEnumerable)propValue)
                            props.Add(p.Name + "=" + HttpUtility.UrlEncode(item.ToString()));
                        break;
                    default:
                        props.Add(p.Name + "=" + HttpUtility.UrlEncode(propValue.ToString()));
                        break;
                }
            }

            return "?" + String.Join("&", props.ToArray());
        }
    }

}
