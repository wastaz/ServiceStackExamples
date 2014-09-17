using System.Collections.Generic;
using System.Linq;
using Raven.Client;
using Raven.Client.Linq;
using ServiceStack;
using _2_Consuming_Shared;

namespace _2_Consuming_Server.Services {
    internal class AnimalService : Service {
        private readonly IDocumentStore documentStore;
        public AnimalService(IDocumentStore documentStore) {
            this.documentStore = documentStore;
        }

        public IList<AnimalResponse> Get(FindAnimals request) {
            var results = new List<AnimalResponse>();
            using(var session = documentStore.OpenSession()) {
                var animals = session.Query<Animal>().Where(a => a.Name.StartsWith(request.Name)).ToList();
                results.AddRange(animals.Select(a => new AnimalResponse { Name = a.Name, Race = a.Race, Noise = GetNoise(a.Race) }));
            }
            return results;
        }

        public AnimalResponse Get(GetAnimal request) {
            using(var session = documentStore.OpenSession()) {
                var result = session.Query<Animal>().First(a => a.Name.Equals(request.Name));
                return new AnimalResponse {Name = result.Name, Race = result.Race, Noise = GetNoise(result.Race)};
            }
        }

        public AnimalResponse Post(Animal request) {
            using(var session = documentStore.OpenSession()) {
                session.Store(request);
                session.SaveChanges();
            }
            return new AnimalResponse {
                                          Name = request.Name, 
                                          Race = request.Race, 
                                          Noise = GetNoise(request.Race)
                                      };
        }

        public IList<AnimalResponse> Post(AnimalCollection request) {
            return request.Animals.Select(Post).ToList();
        }

        private string GetNoise(Race race) {
            switch(race) {
                case Race.Cat:
                    return "Meow";
                case Race.Dog:
                    return "Woof";
                case Race.Rat:
                    return "Squeak";
                case Race.Turtle:
                default:
                    return "...";
            }
        }
    }
}
