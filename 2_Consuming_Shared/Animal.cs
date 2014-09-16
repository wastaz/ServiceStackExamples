using System.Collections.Generic;
using ServiceStack;

namespace _2_Consuming_Shared {
    public enum Race {
        None = 0,
        Cat,
        Dog,
        Turtle,
        Rat,
    }

    [Route("/animals/", "GET")]
    public class FindAnimals : IReturn<List<AnimalResponse>> {
        public string Name { get; set; }
    }

    [Route("/animal/{Name}", "GET")]
    public class GetAnimal : IReturn<AnimalResponse> {
        public string Name { get; set; }
    }

    [Route("/animal", "POST")]
    public class Animal : IReturn<AnimalResponse> {
        public string Name { get; set; }
        public Race Race { get; set; }
    }

    [Route("/animals", "POST")]
    public class AnimalCollection : IReturn<List<AnimalResponse>> {
        public AnimalCollection() {
            Animals = new List<Animal>();
        }
        public List<Animal> Animals { get; set; }
    }

    public class AnimalResponse {
        public string Name { get; set; }
        public Race Race { get; set; }
        public string Noise { get; set; }

        public ResponseStatus ResponseStatus { get; set; }
    }
}
