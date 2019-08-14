using System;
using System.Collections.Generic;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class Offer : Entity<string>
    {
        public string Description { get; private set; }

        public string OfferTypeId { get; private set; }

        public OfferType OfferType { get; private set; }

        public bool Active { get; private set; }

        public string EmployerId { get; set; }

        public Employer Employer { get; set; }

        public List<Candidate> Candidates { get; set; }

        public Offer(string description, string offerTypeId, string employerId)
        {
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException(nameof(description));

            if(string.IsNullOrEmpty(offerTypeId))
                throw new ArgumentNullException("offer");

            if (string.IsNullOrEmpty(employerId))
                throw new ArgumentNullException("employer");

            Id = Guid.NewGuid().ToString();
            Description = description;
            OfferTypeId = offerTypeId;
            Active = false;
            Candidates = new List<Candidate>();
            EmployerId = employerId;
        }

        public void Update(string description, string offerTypeId)
        {
            if(description != Description)
            {
                if (string.IsNullOrEmpty(description))
                    throw new ArgumentNullException(nameof(description));
                Description = description;
            }
            if (offerTypeId != OfferTypeId)
            {
                if (string.IsNullOrEmpty(offerTypeId))
                    throw new ArgumentNullException("offer");
                OfferTypeId = offerTypeId;
            }
        }

        public void SetActive(bool active)
        {
            Active = active;
        }

        public override bool Equals(object obj)
        {
            var otherOffer = obj as Offer;

            if (otherOffer == null)
                return false;

            if (ReferenceEquals(this, otherOffer))
                return true;

            if (Description.ToLower() == otherOffer.Description.ToLower() && OfferTypeId == otherOffer.OfferTypeId)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return (GetType().ToString() + Id.ToString() + Description.ToLower() + OfferTypeId).GetHashCode();
        }
    }
}
