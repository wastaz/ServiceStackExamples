using System.Collections;
using System.Collections.Generic;
using Funq;
using Raven.Client;
using Raven.Client.Embedded;
using ServiceStack;
using _2_Consuming_Server.Services;
using _2_Consuming_Shared;

namespace _2_Consuming_Server {
    internal class AppHost : AppHostHttpListenerBase {
        public AppHost() : base("HttpListener Self-Host", typeof(AnimalService).Assembly) { }

        public override void Configure(Container container) {
            var documentStore = new EmbeddableDocumentStore {
                                                                DataDirectory = "Raven",
                                                                UseEmbeddedHttpServer = true,
                                                                RunInMemory = true,
                                                            };
            documentStore.Initialize();
            container.Register<IDocumentStore>(documentStore);

            Routes.Add<IList<Animal>>("/animal", "POST");
        }
    }
}
