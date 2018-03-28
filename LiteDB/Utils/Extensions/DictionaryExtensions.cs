using System;
using System.Collections.Generic;

namespace LiteDB
{
    internal static class DictionaryExtensions
    {
        public static ushort NextIndex<T>(this Dictionary<ushort, T> dict)
        {
            ushort next = 0;

            while (dict.ContainsKey(next))
            {
                next++;
            }

            return next;
        }

        public static T GetOrDefault<K, T>(this IDictionary<K, T> dict, K key, T defaultValue = default(T))
        {
            T result;

            if (dict.TryGetValue(key, out result))
            {
                return result;
            }

            return defaultValue;
        }

        public static void ParseKeyValue(this IDictionary<string, string> dict, string connectionString)
        {
            int currentPos = 0;
            while (currentPos < connectionString.Length)
            {
                int ePos = connectionString.IndexOf('=', currentPos);
                if (ePos != -1)
                {
                    int termPos = connectionString.IndexOf(';', ePos);
                    var key = connectionString.Substring(currentPos, ePos - currentPos);
                    key = key.Trim(new char[] { ' ', '\"' });
                    if (termPos != -1)
                    {
                        dict[key] = connectionString.Substring(ePos + 1, termPos - ePos - 1);
                        currentPos = termPos + 1;
                    }
                    else
                    {
                        var precleaned = connectionString.Substring(ePos + 1);
                        dict[key] = precleaned.Trim(new char[] { ' ', '\"' });
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
        }
    }
}