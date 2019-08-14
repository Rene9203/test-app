using System;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class OfferType : Entity<string>
    {
        public string Name { get; private set; }

        public OfferType(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
            Id = Guid.NewGuid().ToString();
        }

        public void UpdateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));
            Name = name;
        }
    }
}
