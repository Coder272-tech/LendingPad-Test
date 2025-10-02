using System.Reflection;
using BusinessEntities;
using Common;
using Data.Repositories;
using Data.UnitOfWork;
using Raven.Client;
using Raven.Client.Document;
using Raven.Client.Indexes;
using Raven.Imports.Newtonsoft.Json;
using SimpleInjector;

namespace Data
{
    public class DataConfiguration
    {
        public static void Initialize(Container container, Lifestyle lifestyle, bool createIndexes = true, bool useRaven = true)
        {
            container.Options.AllowOverridingRegistrations = true;
            var assembly = typeof(DataConfiguration).Assembly;

            container.RegisterSingleton<IListTypeLookup<Assembly>, ListTypeLookup<Assembly>>();

            InitializeAssemblyInstancesService.RegisterAssemblyWithSerializableTypes(container, typeof(User).Assembly);
            InitializeAssemblyInstancesService.RegisterAssemblyWithSerializableTypes(container, assembly);

            InitializeAssemblyInstancesService.Initialize(container, lifestyle, assembly);

            if (useRaven)
            {
                container.RegisterSingleton(() => InitializeDocumentStore(assembly, createIndexes));

                container.Register(() =>
                {
                    var session = container.GetInstance<IDocumentStore>().OpenSession();
                    session.Advanced.MaxNumberOfRequestsPerSession = 5000;
                    return session;
                }, lifestyle);
                container.Register<IUnitOfWork, RavenUnitOfWork>(lifestyle);
            }
            else
            {
                container.Register<IUnitOfWork, InMemoryUnitOfWork>(lifestyle);
                // --------------------------
                // 🔹 In-memory repository registrations
                // --------------------------
                container.Register<IUserRepository, InMemoryUserRepository>(Lifestyle.Singleton);

                container.Register<IProductRepository, InMemoryProductRepository>(Lifestyle.Singleton);
                container.Register<IOrderRepository, InMemoryOrderRepository>(Lifestyle.Singleton);
            }

        }

        private static IDocumentStore InitializeDocumentStore(Assembly assembly, bool createIndexes)
        {
            var documentStore = new DocumentStore
                                {
                                    Url = "http://localhost:8080/",
                                    DefaultDatabase = "SampleProject",
                                    Conventions =
                                    {
                                        DefaultUseOptimisticConcurrency = true,
                                        DocumentKeyGenerator = (dbname, commands, entity) => "",
                                        SaveEnumsAsIntegers = true,
                                        CustomizeJsonSerializer = serializer =>
                                                                  {
                                                                      serializer.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                                                                      serializer.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
                                                                      serializer.DefaultValueHandling = DefaultValueHandling.Ignore;
                                                                      serializer.DateFormatHandling = DateFormatHandling.IsoDateFormat;
                                                                      serializer.NullValueHandling = NullValueHandling.Include;
                                                                  },
                                    }
                                };

            documentStore.Initialize();

            if (createIndexes)
            {
                IndexCreation.CreateIndexes(assembly, documentStore);
            }

            return documentStore;
        }
    }
}