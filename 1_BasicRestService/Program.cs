using Funq;
using ServiceStack;
using Topshelf;

namespace _1_BasicRestService {
    [Route("/hello/{Name}")]
    internal class Hello : IReturn<HelloResponse> {
        public string Name { get; set; }
    }

    internal class HelloResponse {
        public string HelloPhrase { get; set; }
    }

    internal class HelloService : Service {
        public HelloResponse Any(Hello request) {
            return new HelloResponse {HelloPhrase = "Hello {0}".Fmt(request.Name),};
        }
    }

    internal class AppHost : AppHostHttpListenerBase {
        public AppHost()
            : base("HttpListener Self-Host", typeof (HelloService).Assembly) {}

        public override void Configure(Container container) {}
    }

    internal class Program {
        private static void Main(string[] args) {
            HostFactory.Run(x => {
                                x.Service<AppHost>(s => {
                                                       s.ConstructUsing(name => new AppHost());
                                                       s.WhenStarted(ah => {
                                                                         ah.Init();
                                                                         ah.Start("http://*:1337/");
                                                                     });
                                                       s.WhenStopped(ah => ah.Stop());
                                                   });
                                x.RunAsLocalSystem();
                                x.SetDescription("Basic REST Service example");
                                x.SetDisplayName("Basic REST Service");
                                x.SetServiceName("BasicRestService");
                                x.StartAutomatically();
                            });
        }
    }
}