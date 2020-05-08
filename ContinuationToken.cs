using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace StackExchangeRedisExtension
{
    internal class ContinuationToken
    {
        public string Cursor { get; set; } = 0.ToString();

        public override string ToString()
        {
            var jsonStr = JsonSerializer.Serialize(this); ;
            var plainTextBytes = Encoding.UTF8.GetBytes(jsonStr);
            return Convert.ToBase64String(plainTextBytes);
        }

        internal static ContinuationToken Parse(string inputStr)
        {
            var continuationToken = new ContinuationToken();
            if (string.IsNullOrWhiteSpace(inputStr))
            {
                return continuationToken;
            }

            var base64EncodedBytes = Convert.FromBase64String(inputStr);
            inputStr = Encoding.UTF8.GetString(base64EncodedBytes);

            try
            {
                continuationToken = JsonSerializer.Deserialize<ContinuationToken>(inputStr);
            }
            catch
            {
                continuationToken = new ContinuationToken();
            }

            return continuationToken;

        }
    }
}
