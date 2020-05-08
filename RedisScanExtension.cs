using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StackExchangeRedisExtension
{
    public static class RedisScanExtension
    {
        public static async Task<ListResponse<string>> SetScanAsync(this IDatabase db, RedisKey key, string pattern, string continueationToken = "")
        {
            var token = ContinuationToken.Parse(continueationToken);
            var scanRes = await db.ExecuteAsync("sscan", key, token.Cursor, "match", pattern, "count", 20);
            var scanPair = scanRes.ToDictionary().First();
            token = new ContinuationToken() { Cursor = scanPair.Key };
            var resValue = (string[])scanPair.Value;
            return new ListResponse<string>(resValue, token.ToString());
        }

        public static async Task<ListResponse<KeyValuePair<string, string>>> HashScanAsync(this IDatabase db, RedisKey key, string pattern, string continueationToken = "")
        {
            var token = ContinuationToken.Parse(continueationToken);
            var scanRes = await db.ExecuteAsync("hscan", key, token.Cursor, "match", pattern, "count", 20);
            var scanPair = scanRes.ToDictionary().First();
            token = new ContinuationToken() { Cursor = scanPair.Key };
            var resValue = (string[])scanPair.Value;
            var list = new List<KeyValuePair<string, string>>();
            for (int i = 0; i < resValue.Length - 1; i = i +2)
            {
                list.Add(new KeyValuePair<string, string>(resValue[i], resValue[i + 1]));
            }

            return new ListResponse<KeyValuePair<string, string>>(list, token.ToString());
        }
    }
}
