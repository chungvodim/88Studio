using DryIoc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tearc.Repository;

namespace Tearc.Bootstrapper
{
    public class DryIoC
    {
        public static void Configure(IContainer container)
        {
            // Register all the services interface/implement here
            // The services used by clients should be abstract. DryIoC will be the one manage the actual implementation
            // Service that used Sql and MongoDb should be separated to avoid confusion

            // Sql EF services
            container.Register<ApplicationDbContext>(Reuse.Singleton);
            container.Register(typeof(IRepository<>), typeof(Repository<>), Reuse.Transient);
        }
    }
}
