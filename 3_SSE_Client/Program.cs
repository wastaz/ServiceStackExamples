using System;
using System.Threading.Tasks;
using ServiceStack;
using _3_SSE_Shared;

namespace _3_SSE_Client {
    public class GlobalReciever : ServerEventReceiver {
        public void Broadcast(SseBroadcastedMessage request) {
            Console.Out.WriteLine("({0})> {1}", request.Name, request.Message);
        }
    }

    class Program {
        static void Main(string[] args) {
            string name;
            for(;;) {
                Console.Out.Write("Name: ");
                name = Console.In.ReadLine();
                if(!string.IsNullOrWhiteSpace(name)) {
                    break;
                }
            }

            var sseClient = new ServerEventsClient("http://localhost:1337/");
            sseClient.RegisterReceiver<GlobalReciever>();
            sseClient.OnCommand = sem => {
                                      var joinEvent = sem as ServerEventJoin;
                                      if(joinEvent != null) {
                                          Console.Out.WriteLine("{0} joined...".Fmt(joinEvent.DisplayName));
                                      }

                                      var leaveEvent = sem as ServerEventLeave;
                                      if(leaveEvent != null) {
                                          var jnevent = leaveEvent.Json.FromJson<ServerEventJoin>();
                                          Console.Out.WriteLine("{0} left...".Fmt(jnevent.DisplayName));
                                      }
                                  };
            Task.WaitAll(sseClient.Connect());

            for(;;) {
                Console.Out.Write("> ");
                var command = Console.In.ReadLine();
                if(string.IsNullOrWhiteSpace(command)) {
                    continue;
                }
                command = command.Trim();

                if(command.Trim() == "/quit") {
                    break;
                }

                sseClient.ServiceClient.Post(new BroadcastRequest {Name = name, Message = command});
            }
        }
    }
}
