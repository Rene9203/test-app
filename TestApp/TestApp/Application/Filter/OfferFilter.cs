using System;
using System.Linq.Expressions;
using TestApp.Application.ViewModels;
using TestApp.Core.Entities;

namespace TestApp.Application.Filter
{
    public class OfferFilter
    {
        public static Expression<Func<Offer, bool>> ConvertTo(OfferViewModelFilter filter)
        {
            Expression<Func<Offer, bool>> expression = t => true;
            if (filter == null) return expression;

            expression = GetOfferTypeFilter(expression, filter.OfferTypeId);
            return expression;
        }

        private static Expression<Func<Offer, bool>> GetOfferTypeFilter(Expression<Func<Offer, bool>> expression, string offerTypeId)
        {
            if (string.IsNullOrEmpty(offerTypeId)) return expression;
            Expression<Func<Offer, bool>> condition = o => o.OfferTypeId.Equals(offerTypeId);
            return expression.AndAlso(condition);
        }

        private static Expression<Func<Offer, bool>> GetActiveFilter(Expression<Func<Offer, bool>> expression, bool active)
        {
            Expression<Func<Offer, bool>> condition = o => o.Active == active;
            return expression.AndAlso(condition);
        }
    }
}
