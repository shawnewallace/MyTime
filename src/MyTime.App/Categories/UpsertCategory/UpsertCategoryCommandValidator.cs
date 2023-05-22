using FluentValidation;

namespace MyTime.App.Categories.UpsertCategory
{
	public class UpsertCategoryCommandValidator : AbstractValidator<UpsertCategoryEntryCommand>
	{
		public UpsertCategoryCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
	}
}