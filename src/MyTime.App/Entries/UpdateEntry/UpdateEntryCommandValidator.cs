using FluentValidation;

namespace MyTime.App.Entries.UpdateEntry;

public class UpdateEntryCommandValidator : AbstractValidator<UpdateEntryCommand>
{
  public UpdateEntryCommandValidator()
  {
    RuleFor(x => x.Description)
      .MaximumLength(255);

    RuleFor(x => x.Duration)
      .GreaterThanOrEqualTo(0.0F)
      .LessThan(24.0F);
  }
}
