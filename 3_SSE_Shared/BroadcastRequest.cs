using ServiceStack;

namespace _3_SSE_Shared {
    [Route("/broadcast")]
    public class BroadcastRequest : IReturnVoid {
        public string Name { get; set; }
        public string Message { get; set; }
    }

    public class SseBroadcastedMessage {
        public string Name { get; set; }
        public string Message { get; set; }
    }
}
