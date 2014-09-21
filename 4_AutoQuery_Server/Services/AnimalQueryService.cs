using ServiceStack;
using _4_AutoQuery_Shared;

namespace _4_AutoQuery_Server.Services {
    internal class AnimalQueryService : Service {
        private readonly IAutoQuery autoQuery;

        public AnimalQueryService(IAutoQuery autoQuery) {
            this.autoQuery = autoQuery;
        }

        public object Any(FindAnimals request) {
            return DoQuery(request);
        }

        public object Any(FindOwners request) {
            return DoQuery(request);
        }

        public object Any(FindHouses request) {
            return DoQuery(request);
        }

        public object Any(FindFullAnimalInfo request) {
            return DoQuery(request);
        }

        public QueryResponse<TBaseType> DoQuery<TBaseType>(
            IQuery<TBaseType> query) {

            var q = autoQuery.CreateQuery(query, Request.GetRequestParams());
            return autoQuery.Execute(query, q);

        }

        public QueryResponse<TResultType> DoQuery<TBaseType, TResultType>(
            IQuery<TBaseType, TResultType> query) {

            var q = autoQuery.CreateQuery(query, Request.GetRequestParams());
            return autoQuery.Execute(query, q);

        }
    }
}
