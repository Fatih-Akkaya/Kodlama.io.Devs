using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Commands.CreateTechnology
{
    public class CreateTechnologyValidator : AbstractValidator<CreateTechnologyCommand>
    {
        public CreateTechnologyValidator()
        {
            RuleFor(c => c.Name).NotEmpty();
            RuleFor(c => c.Name).MinimumLength(2);
        }
    }
}
