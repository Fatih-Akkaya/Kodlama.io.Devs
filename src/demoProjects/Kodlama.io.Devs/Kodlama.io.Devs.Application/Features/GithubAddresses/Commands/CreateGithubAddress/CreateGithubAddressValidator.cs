using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.GithubAddresses.Commands.CreateGithubAddress
{
    public class CreateGithubAddressValidator : AbstractValidator<CreateGithubAddressCommand>
    {
        public CreateGithubAddressValidator()
        {
            RuleFor(c => c.GithubUrl).NotEmpty();
            RuleFor(c => c.GithubUrl).Must(x => x.Contains("github.com/"));
            RuleFor(c => c.GithubUrl).MinimumLength(2);
        }
    }
}
