using FluentValidation;
using TaskManagement.Application.DTOs;

namespace TaskManagement.Application.Validators
{
    public class CreateTaskDtoValidator : AbstractValidator<CreateTaskDto>
    {
        public CreateTaskDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.DueDate)
                .GreaterThan(DateTime.UtcNow)
                .WithMessage("Due date must be in the future");
        }
    }
}