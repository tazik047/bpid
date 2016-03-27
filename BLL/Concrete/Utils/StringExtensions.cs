using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace BLL.Concrete.Utils
{
    public static class StringExtensions
    {
        public static T ToObject<T>(this string target)
        {
            return JObject.Parse(target).ToObject<T>();
        }
    }
}
