using ServiceStack.FluentValidation;
using _2_Consuming_Shared;

namespace _2_Consuming_Server.Validation {
    internal class AnimalCollectionValidator : AbstractValidator<AnimalCollection> {
        public AnimalCollectionValidator() {
            RuleFor(x => x.Animals).NotEmpty().WithMessage("Must provide at least one animal!");
            RuleFor(x => x.Animals).SetCollectionValidator(new AnimalValidator());
        }
    }

    internal class AnimalValidator : AbstractValidator<Animal> {
        public AnimalValidator() {
            RuleFor(x => x.Name).NotEqual("Party badger")
                                .WithErrorCode("PartyBadgerNotAllowed")
                                .WithMessage("The party badger is banned for life!");
        }
    }
}
