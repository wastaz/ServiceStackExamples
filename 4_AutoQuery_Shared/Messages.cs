using System.Collections.Generic;
using ServiceStack;
using ServiceStack.DataAnnotations;
using ServiceStack.Model;

namespace _4_AutoQuery_Shared {

    [Route("/animals", "GET")]
    public class FindAnimals : QueryBase<Animal> {
        public AnimalType[] AnimalTypes { get; set; }
    }

    [Route("/owners", "GET")]
    public class FindOwners : QueryBase<Owner> {
        public string NameContains { get; set; }
    }

    [Route("/houses", "GET")]
    public class FindHouses : QueryBase<House> {
        public string AdressStartsWith { get; set; }
    }

    [Route("/fullanimalinfo", "GET")]
    public class FindFullAnimalInfo :QueryBase<Animal, FullAnimalInfo>, IJoin<Animal, Owner, House> { }

    public class FullAnimalInfo {
        public string AnimalName { get; set; }
        public string OwnerName { get; set; }
        public string HouseAdress { get; set; }
    }

    public class Animal : IHasIntId {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        [StringLength(3, 100)]
        public string Name { get; set; }

        [Required]
        public AnimalType AnimalType { get; set; }

        [ForeignKey(typeof(Owner), OnDelete = "SET NULL")]
        public int OwnerId { get; set; }
    }

    public class Owner : IHasIntId {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        [StringLength(3, 100)]
        public string Name { get; set; }

        [Reference]
        public List<Animal> OwnedAnimals { get; set; }

        [ForeignKey(typeof(House), OnDelete = "SET NULL")]
        public int HouseId { get; set; }
    }

    public class House : IHasIntId {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }

        [Required]
        public string Adress { get; set; }

        [Reference]
        public List<Owner> PeopleLivingHere { get; set; }
    }

    public enum AnimalType {
        None = 0,
        Dog,
        Cat,
        Turtle,
        Rat,
    }
}
