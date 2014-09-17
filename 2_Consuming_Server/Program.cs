using Topshelf;

namespace _2_Consuming_Server {
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
