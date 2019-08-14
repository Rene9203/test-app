using System;
using System.Collections.Generic;
using TestApp.Core.SharedKernel;

namespace TestApp.Core.Entities
{
    public class Employer : User
    {
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public string FullName => $"{FirstName} {LastName}";

        public List<Offer> Offers { get; private set; }

        public Employer(string firstName, string lastName, string userName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (string.IsNullOrEmpty(userName))
                throw new ArgumentNullException(nameof(userName));

            Id = Guid.NewGuid().ToString();
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Offers = new List<Offer>();
        }

        public Employer SetPassword(string password)
        {
            var passwordHash = 
            PasswordHash = password;
            return this;
        }

        public void Update(string firstName, string lastName)
        {
            if (string.IsNullOrEmpty(firstName))
                throw new ArgumentNullException(nameof(firstName));

            FirstName = firstName;
            LastName = lastName;
        }
    }
}
