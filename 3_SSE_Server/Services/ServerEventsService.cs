using ServiceStack;
using _3_SSE_Shared;

namespace _3_SSE_Server.Services {
    internal class ServerEventsService : Service {
        private readonly IServerEvents serverEvents;

        public ServerEventsService(IServerEvents serverEvents) {
            this.serverEvents = serverEvents;
        }

        public void Post(BroadcastRequest request) {
            serverEvents.NotifyAll(request.ConvertTo<SseBroadcastedMessage>());
        }
    }
}
