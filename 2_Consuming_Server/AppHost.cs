using Funq;
using Raven.Client;
using Raven.Client.Embedded;
using ServiceStack;
using ServiceStack.Auth;
using ServiceStack.Authentication.RavenDb;
using ServiceStack.Caching;
using ServiceStack.Validation;
using _2_Consuming_Server.Services;

namespace _2_Consuming_Server {
    internal class AppHost : AppHostHttpListenerBase {
        public AppHost() : base("HttpListener Self-Host", typeof(AnimalService).Assembly) { }

        public override void Configure(Container container) {
            var documentStore = new EmbeddableDocumentStore {
                                                                DataDirectory = "Raven",
                                                                RunInMemory = true,
                                                            };
            documentStore.Initialize();
            container.Register<IDocumentStore>(documentStore);

            container.Register<ICacheClient>(new MemoryCacheClient());

            Plugins.Add(new AuthFeature(() => new AuthUserSession(), new IAuthProvider[] {
                new BasicAuthProvider(), 
            }));
            container.Register<IUserAuthRepository>(new RavenDbUserAuthRepository(Container.Resolve<IDocumentStore>()));

            Plugins.Add(new ValidationFeature());
            container.RegisterValidators(typeof(AppHost).Assembly);

            Plugins.Add(new CorsFeature());
            Plugins.Add(new PostmanFeature());


            var userAuthRepo = container.Resolve<IUserAuthRepository>();
            userAuthRepo.CreateUserAuth(new UserAuth {UserName = "test"}, "test123");
        }
    }
}
