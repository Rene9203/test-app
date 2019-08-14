using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApp.Application.ViewModels
{
    public class OfferViewDetail
    {
        public string Name { get; set; }

        public int Count { get; set; }

        public static IEnumerable<OfferViewDetail> From(Dictionary<string, int> keyValuePairs)
        {
            var response = new List<OfferViewDetail>();
            foreach (var item in keyValuePairs)
            {
                response.Add(new OfferViewDetail()
                {
                    Name = item.Key,
                    Count = item.Value
                });
            }

            return response;
        }
    }
}
