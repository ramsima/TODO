using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
                .WithMessage("Title is Required!");

            RuleFor(x => x.Title)
                .MaximumLength(80)
                .WithMessage("Title Must be less than 80 chars");

            RuleFor(x => x.DueDate)
                .Must(BeaValidDate)
                .When(x => x.DueDate.HasValue)
                .WithMessage("Due date Cannot be in the past!");

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

        }

        private static bool BeaValidDate(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            return date.Value.Date <= DateTime.UtcNow;
        }

    }
}
