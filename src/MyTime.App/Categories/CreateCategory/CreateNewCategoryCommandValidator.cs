using FluentValidation;

namespace MyTime.App.Categories.CreateNewCategory;

public class CreateNewCategoryCommandValidator : AbstractValidator<CreateNewCategoryCommand>
{
  public CreateNewCategoryCommandValidator() => RuleFor(x => x.Name).MaximumLength(50);
}
