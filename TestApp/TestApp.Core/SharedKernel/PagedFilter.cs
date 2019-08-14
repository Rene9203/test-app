using System;
using System.Linq.Expressions;

namespace TestApp.Core.SharedKernel
{
    public class PagedFilter<T>
    {
        public Expression<Func<T, bool>> Filter { get; set; }
        public int Page { get; set; }
        public int Limit { get; set; }
        public Expression<Func<T, object>> OrderBy { get; set; }
        public bool Descending { get; set; }

        public PagedFilter(int page, int limit, Expression<Func<T, bool>> filter = null)
        {
            Page = page == 0 ? Constant.DefaultPage : page;
            Limit = limit == 0 ? Constant.DefaultPageLimit : limit;
            Filter = filter ?? (t => true);
        }

        public int GetOmittedPages()
        {
            return Limit * (Page - 1);
        }
    }
}
