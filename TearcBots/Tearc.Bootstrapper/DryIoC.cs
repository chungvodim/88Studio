using DryIoc;
using MongoDB.Driver;
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
            container.Register<ApplicationDbContext>(Reuse.ScopedOrSingleton);
            container.Register<IEntityFrameWorkRepository, EntityFrameWorkRepository>(Reuse.Transient);

            // Mongo service
            container.Register<IMongoRepository, MongoRepository>(Reuse.Transient);
            container.RegisterDelegate<IMongoDatabase>(x => MongoDatabaseFactory.Create(), Reuse.ScopedOrSingleton);
        }
    }
}
