using DataAccessLayer;
using DataAccessLayer.Interfaces;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Infrastructure
{
    public class ServiceModule: NinjectModule
    {
        private DbContextOptions<DataContext> options;
        private Func<object, object> p;

        public ServiceModule(DbContextOptions<DataContext> options)
        {
            this.options = options;
        }

        public ServiceModule(Func<object, object> p)
        {
            this.p = p;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(options);
        }
    }
}
