using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchangeRedisExtension
{
    public class ListResponse<T>
    {
        public IEnumerable<T> Items { get; }

        public string ContinuationToken { get; }

        public ListResponse(IEnumerable<T> items, string continuationToken)
        {
            Items = items;
            ContinuationToken = continuationToken;
        }
    }
}
