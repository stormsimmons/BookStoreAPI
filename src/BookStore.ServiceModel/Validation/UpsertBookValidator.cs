using BookStore.ServiceModel.Requests;
using FluentValidation;

namespace BookStore.ServiceModel.Validation
{
    public class UpsertBookValidator : AbstractValidator<UpsertBookRequest>
    {
        public UpsertBookValidator()
        {
            RuleFor(x => x.Book.Title).NotNull().NotEmpty();
        }
    }
}
