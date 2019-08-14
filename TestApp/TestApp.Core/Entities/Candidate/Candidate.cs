using System;
using System.Text.RegularExpressions;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class Candidate : Entity<string>
    {
        public string Name { get; private set; }

        public string Email { get; set; }

        public string OfferId { get; private set; }

        public Offer Offer { get; private set; }

        public Candidate(string name, string email, string offerId)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            if (string.IsNullOrEmpty(offerId))
                throw new ArgumentNullException("offer");

            if (string.IsNullOrEmpty(email))
                throw new ArgumentNullException(nameof(email));

            Id = Guid.NewGuid().ToString();
            Name = name;
            OfferId = offerId;
            Email = email;
        }
    }
}
