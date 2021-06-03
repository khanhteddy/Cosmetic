using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cosmetic.Helper
{
    public static class SessionHelper
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            string sessionValue = JsonConvert.SerializeObject(value);

            session.SetString(key, sessionValue);
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            string sessionValue = session.GetString(key);

            T obj = string.IsNullOrEmpty(sessionValue) ? default(T) : JsonConvert.DeserializeObject<T>(sessionValue);

            return obj;
        }
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
