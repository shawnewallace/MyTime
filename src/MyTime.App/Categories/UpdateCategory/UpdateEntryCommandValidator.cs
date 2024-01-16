using FluentValidation;

namespace MyTime.App.Categories.UpdateCategory;

public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommand>
{
  public UpdateCategoryCommandValidator()
  {
    RuleFor(x => x.Name).MaximumLength(50);
  }
}
