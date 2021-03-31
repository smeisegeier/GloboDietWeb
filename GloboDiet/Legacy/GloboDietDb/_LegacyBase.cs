using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Legacy.GloboDietDb
{
    public abstract class _LegacyBase
    {

        public _LegacyBase() { }

        /// <summary>
        /// Gets all lines from SQL table as List of this object. 
        /// </summary>
        /// <remarks>
        /// This is done as static, strongly typed method.
        /// Static objects cannot reflect its base at runtime, so the derived class must be passed as type.
        /// List Type construct needs Type at compile time (?), so non-static method still dont work here.
        /// </remarks>
        /// <typeparam name="T">SQL table name</typeparam>
        /// <returns>Enumerable of all objects</returns>
        public static IEnumerable<T> GetLegacyObjects<T>() where T : _LegacyBase
        {
            return JsonConvert.DeserializeObject<List<T>>(
                File.ReadAllText(Path.Combine(
                    "Legacy/GloboDietDb", typeof(T).Name + ".json")
                )
            );
        }


        // OO approach needs far more invest. DEPR.
        // example --> var legacyList = new POC().GetLegacyObjects() as IEnumerable<POC>; // wont work
        //public IEnumerable<_LegacyBase> GetLegacyObjects()
        //{
        //    return JsonConvert.DeserializeObject<List<_LegacyBase>>(
        //        File.ReadAllText(Path.Combine(
        //            "Legacy/GloboDietDb", this.GetType().Name + ".json")
        //        )
        //    );
        //}


    }
}
