using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestApp.Core.Entities.Repositories;
using TestApp.Core.Repositories;
using TestApp.Core.Service;
using TestApp.Infraestructure.Repositories;

namespace TestApp.Infraestructure.AutofacModule
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterGeneric(typeof(GenericRepository<,>))
                .As(typeof(IRepository<,>))
                .InstancePerLifetimeScope();

            #region Repositories Registretion
            builder.RegisterType<OfferRepository>()
                .As<IOfferRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<EmployerRepository>()
               .As<IEmployerRepository>()
               .InstancePerLifetimeScope();
            builder.RegisterType<RoleRepository>()
              .As<IRoleRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<CandidateRepository>()
              .As<ICandidateRepository>()
              .InstancePerLifetimeScope();
            builder.RegisterType<OfferTypeRepository>()
             .As<IOfferTypeRepository>()
             .InstancePerLifetimeScope();
            #endregion

            #region Service Registration
            builder.RegisterType<OfferService>()
                .As<OfferService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<OfferTypeService>()
               .As<OfferTypeService>()
               .InstancePerLifetimeScope();
            #endregion

        }
    }
}
