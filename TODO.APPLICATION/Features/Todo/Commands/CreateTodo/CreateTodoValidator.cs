using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TODO.APPLICATION.Features.Todo.Commands.CreateTodo
{
    public class CreateTodoValidator : AbstractValidator<CreateTodoCommand>
    {
        public CreateTodoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required");

            RuleFor(x => x.Title)
                .MaximumLength(80)
                .WithMessage("Maximum Length is 80 characters!");

            RuleFor(x => x.Description)
                .MaximumLength(1000)
                .WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.CategogryId)
                .GreaterThan(0)
                .WithMessage("A valid category must be selected.");

            RuleFor(x => x.PriorityId)
                .GreaterThan(0)
                .WithMessage("A valid priority must be selected.");


                

            RuleForEach(x => x.TagIds)
                .GreaterThan(0)
                .WithMessage("Invalid tag selected.");

            RuleFor(x => x.DueDate)
                .NotEmpty()
                .Must(BeAValidDate)
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date cannot be in the past.");
        }

        private static bool BeAValidDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date.Value.Date <= DateTime.Today;
        }
    }
}
