using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ServiceStack;
using ServiceStack.Text;
using _2_Consuming_Shared;

namespace _2_Consuming_Client {
    internal class Program {
        static void Main(string[] args) {
            var client = new JsonServiceClient("http://localhost:1337/") {
                UserName = "test",
                Password = "test123",
            };
            Task.WaitAll(DoStuff(client));
            Console.In.ReadLine();
        }

        static async Task DoStuff(IServiceClient client) {
            var response = await client.PostAsync(
                new AnimalCollection {
                    Animals = new List<Animal> {
                        new Animal {Name = "Pipen", Race = Race.Cat},
                        new Animal {Name = "Morris", Race = Race.Dog},
                        new Animal {Name = "Grävling", Race = Race.Cat},
                        new Animal {Name = "A'tuin", Race = Race.Turtle},
                        new Animal {Name="Merry", Race = Race.None},
                        new Animal {Name="Pippin", Race = Race.None},
                    }
                });
            response.PrintDump();

            var queryResponse = await client.GetAsync(new FindAnimals {Name = "Pi"});
            queryResponse.PrintDump();
        }
    }
}
