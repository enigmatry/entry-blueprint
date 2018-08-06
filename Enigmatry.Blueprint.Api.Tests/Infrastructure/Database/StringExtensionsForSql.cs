using System;
using System.Collections.Generic;
using System.Linq;
using Enigmatry.Blueprint.Core.Helpers;

namespace Enigmatry.Blueprint.Api.Tests.Infrastructure.Database
{
    public static class StringExtensionsForSql
    {
        public static string[] SplitStatements(this string sql)
        {
            var sqlBatch = string.Empty;
            var result = new List<string>();
            sql += "\nGO"; // make sure last batch is executed.

            foreach (string line in sql.Split(new[] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries))
            {
                if (line.ToUpperInvariant().Trim() == "GO")
                {
                    result.Add(sqlBatch);
                    sqlBatch = string.Empty;
                }
                else
                {
                    sqlBatch += line + "\n";
                }
            }

            return result.Where(s => s.HasContent()).ToArray();
        }
    }
}
