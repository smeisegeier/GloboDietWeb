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
        /// Gets all lines from SQL table as List of this object
        /// </summary>
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
    }
}
