using System.Collections.Generic;
using Funq;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.OrmLite;
using _4_AutoQuery_Shared;
using Owner = _4_AutoQuery_Shared.Owner;

namespace _4_AutoQuery_Server {
    internal class AppHost : AppHostHttpListenerBase {
        public AppHost() : base("HttpListener Self-Host", typeof(AppHost).Assembly) { }

        public override void Configure(Container container) {
            LogManager.LogFactory = new ConsoleLogFactory();

            container.Register<IDbConnectionFactory>(
                new OrmLiteConnectionFactory(":memory:", SqliteDialect.Provider));

            Plugins.Add(new CorsFeature());
            Plugins.Add(new AutoQueryFeature {
                MaxLimit = 100
            });
            Plugins.Add(new PostmanFeature());

            SetupDatabase(container.Resolve<IDbConnectionFactory>());
        }

        private void SetupDatabase(IDbConnectionFactory dbFactory) {
            using(var db = dbFactory.OpenDbConnection()) {
                db.CreateTables(
                    true, 
                    typeof(Animal), 
                    typeof(Owner), 
                    typeof(House));

                var pipen = new Animal {
                    AnimalType = AnimalType.Cat,
                    Name = "Pipen",
                };
                var gravling = new Animal {
                    AnimalType = AnimalType.Cat, 
                    Name = "Grävling",
                };
                var morris = new Animal {
                    AnimalType = AnimalType.Dog, 
                    Name = "Morris",
                };
                db.SaveAll(new[] {pipen, gravling, morris});

                var fredrik = new Owner {
                    Name = "Fredrik", 
                    OwnedAnimals = new List<Animal> {pipen, gravling, morris},
                };
                db.Save(fredrik, true);

                var kungligaslottet = new House {
                    Adress = "Internet",
                    PeopleLivingHere = new List<Owner> {fredrik}
                };
                db.Save(kungligaslottet, true);
            }
        }
    }
}
