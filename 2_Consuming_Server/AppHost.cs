using Funq;
using Raven.Client;
using Raven.Client.Embedded;
using ServiceStack;
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

            Plugins.Add(new ValidationFeature());
            Container.RegisterValidators(typeof(AppHost).Assembly);

            Plugins.Add(new CorsFeature());
            Plugins.Add(new PostmanFeature());
        }
    }
}
