using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Text;

namespace Navicon.Serializer.DependencyInjection
{
    class IoCModule : NinjectModule
    {
        public override void Load()
        {
          //  Bind<IUserRepository>().To<UserRepository>();
          //  Bind<ICustomerRepository>().To<CustomerRepository>();
          //  Bind<IOrderRepository>().To<OrderRepository>();

            // Bind<IRepositoryFactory>().ToFactory();
        }
    }
}
