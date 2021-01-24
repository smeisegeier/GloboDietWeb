using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet
{
    public static class Extensions
    {
        public enum SqlConnectionType
        {
            LOCAL,
            RKI,
            AZURE,
            INMEMORY,
            UNKNOWN
        }

        /// <summary>
        /// Get currrent Connection Type enum
        /// </summary>
        /// <param name="context">DbContext</param>
        /// <returns>type as enum</returns>
        public static SqlConnectionType GetSqlConnectionType(this DbContext context)
        {
            if (context.Database.IsInMemory())
                return SqlConnectionType.INMEMORY;

            string con = context.Database.GetConnectionString();
            if (con.Contains("mssqllocaldb") || con.Contains("sqlexpress"))
                return SqlConnectionType.LOCAL;
            if (con.Contains("abt2sql") || con.Contains("zfkd"))
                return SqlConnectionType.RKI;
            if (con.Contains("database.windows.net"))
                return SqlConnectionType.AZURE;
            return SqlConnectionType.UNKNOWN;
        }
    }
}
