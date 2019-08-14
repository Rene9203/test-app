using System;
using System.Collections.Generic;

namespace TestApp.Core.SharedKernel
{
    public class PagedResult<T>
    {
        public IEnumerable<T> Data { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public int PagesCount { get; set; }
        public int ElementsCount { get; set; }

        public PagedResult(IEnumerable<T> data, int page, int limit, int elementsCount)
        {
            Page = page;
            Data = data;
            Limit = limit;
            ElementsCount = elementsCount;
            PagesCount = (int)Math.Ceiling(elementsCount * 1.0 / limit * 1.0);
        }
    }
}
