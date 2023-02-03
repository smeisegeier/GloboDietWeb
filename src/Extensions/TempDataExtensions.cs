using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Extensions
{
    /// <summary>
    /// Extends TempData. 
    /// Usage Set: TempData.Set("key", objectA);
    /// Usage Get: var value = TempData.Get<ClassA>("key")
    /// </summary>
    public static class TempDataExtensions
    {
        /// <summary>
        /// Object is stored until next request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void Set<T>(this ITempDataDictionary tempData, string key, T value) where T : class
        {
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Object is marked for deletion after this request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static T Get<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o;
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }

        /// <summary>
        /// Object is marked for deletion after the following request
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tempData"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T Peek<T>(this ITempDataDictionary tempData, string key) where T : class
        {
            object o = tempData.Peek(key);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }


        /// <summary>
        /// Shorthand. Convention: Interview object MUST name "interview"
        /// </summary>
        /// <param name="value">object to cache</param>
        public static void Set(this ITempDataDictionary tempData, Object value)
        {
            string key = value.GetType().Name.ToUpper();
            tempData[key] = JsonConvert.SerializeObject(value);
        }

        /// <summary>
        /// Shorthand. Convention: Interview object MUST name "interview"
        /// </summary>
        /// <typeparam name="T">type of object to retrieve</typeparam>
        public static T Get<T>(this ITempDataDictionary tempData) where T : class
        {
            object o;
            string key = typeof(T).Name.ToUpper();
            tempData.TryGetValue(key, out o);
            return o == null ? null : JsonConvert.DeserializeObject<T>((string)o);
        }
    }
}
