using _88Studio.Repository;
using _88Studio.Repository.BEGAIT;
using _88Studio.Repository.BEGAITData;
using _88Studio.Repository.BEGAITLog;
using _88Studio.Service;
using _88Studio.Service.Cache;
using _88Studio.Service.Implementation;
using BGP.Utils.CacheManager;
using BGP.Utils.CacheManager.MemoryCache;
using DryIoc;
using MongoDB.Driver;
using System;
using System.Data.Entity;

namespace _88Studio.Bootstrapper
{
    public class DryIoC
    {
        public static void Configure(IContainer container, IReuse defaultReuse = null)
        {
            // Register all the services interface/implement here
            // The services used by clients should be abstract. DryIoC will be the one manage the actual implementation
            // Service that used Sql and MongoDb should be separated to avoid confusion
            
            // Choosing cache manager
            container.RegisterDelegate<ICacheManager>(x => new CacheManager("BEGAITCache"), Reuse.Singleton);

            // Sql EF services
            container.Register<BEGAITContext>(defaultReuse);
            container.Register<BEGAITEntityFrameworkRepository>(defaultReuse);
            container.Register<Utils.Service.EntityFramework.BaseService<BEGAITEntityFrameworkRepository>>(defaultReuse);
            container.Register<IAdminstrationService, AdministrationService>(defaultReuse);
            container.Register<IMasterDataService, MasterDataService>(defaultReuse);
            container.Register<IStatisticService, StatisticService>(defaultReuse);
            container.Register<IListingService, ListingService>(defaultReuse);
            container.Register<IImageService, ImageService>(defaultReuse);
            container.Register<ILeadService, LeadService>(defaultReuse);
            container.Register<IImportService, ImportService>(defaultReuse);

            // Log service
            container.Register<BEGAITLogContext>(defaultReuse);
            container.Register<BEGAITLogEntityFrameworkRepository>(defaultReuse);
            container.Register<Utils.Service.EntityFramework.BaseService<BEGAITLogEntityFrameworkRepository>>(defaultReuse);
            container.Register<ILogService, LogService>(defaultReuse);

            // Mongo service
            container.RegisterDelegate<IMongoDatabase>(x => BEGAITMongoDatabaseFactory.Create(), defaultReuse);
            container.Register<BEGAITMongoRepository>(defaultReuse);
            container.Register<Utils.Service.Mongo.BaseService<BEGAITMongoRepository>>(defaultReuse);
        }
    }
}
