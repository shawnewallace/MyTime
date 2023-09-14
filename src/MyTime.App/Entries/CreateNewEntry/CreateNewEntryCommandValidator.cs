using FluentValidation;

namespace MyTime.App.Entries.CreateNewEntry;

public class CreateNewEntryCommandValidator : AbstractValidator<CreateNewEntryCommand>
{
	public CreateNewEntryCommandValidator()
	{
		RuleFor(x => x.OnDate)
			.NotEmpty();

		RuleFor(x => x.Description)
			.NotEmpty()
			.MaximumLength(255);

		RuleFor(x => x.Duration)
			.GreaterThanOrEqualTo(0.0F)
			.LessThan(24.0F);

		RuleFor(x => x.Category)
			.MaximumLength(50);

		RuleFor(x => x.UserId)
			.NotEmpty()
			.MaximumLength(50);
	}
}