using System;
using Funq;
using ServiceStack;
using _3_SSE_Server.Services;

namespace _3_SSE_Server {
    internal class AppHost : AppHostHttpListenerBase {
        public AppHost() : 
            base("HttpListener Self-Host", typeof(ServerEventsService).Assembly) {}

        public override void Configure(Container container) {
            Plugins.Add(new ServerEventsFeature {
                NotifyChannelOfSubscriptions = true
            });
            Plugins.Add(new CorsFeature());
        }
    }
}
