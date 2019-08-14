using System.Collections.Generic;
using TestApp.Core.Entities;
using TestApp.Infraestructure.DbConfig;

namespace TestApp.Infraestructure.Data
{
    public static class SeedData
    {
        public static bool SeedDelepmentData(AppDbContext appContext, string userAdminId, string employerUserId)
        {

            var roleAdministrator = new Role(Role.Administrator);
            roleAdministrator.AddUser(userAdminId);
            var roleEmployer = new Role(Role.Employer);
           
            roleEmployer.AddUser(employerUserId);
            roleEmployer.AddUser(userAdminId);
            appContext.Roles.AddRange(new List<Role> { roleAdministrator, roleEmployer });

            var offerTypeDevOps = new OfferType("DevOps");
            var offerTypeQA = new OfferType("QA");
            var offerTypeDeveloper = new OfferType("Developer");
            var offerTypeManager = new OfferType("Manager");
            var offerTypePM = new OfferType("PM");
            appContext.OfferTypes.AddRange(new List<OfferType> {
                offerTypeDevOps,
                offerTypeQA,
                offerTypeDeveloper,
                offerTypeManager,
                offerTypePM
            });

            return appContext.SaveChanges() >= 0;
        }
    }
}
