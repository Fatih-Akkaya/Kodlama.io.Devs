using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Validation;
using Core.Security.JWT;
using FluentValidation;
using Kodlama.io.Devs.Application.Features.Authorizations.Rules;
using Kodlama.io.Devs.Application.Features.GithubAddresses.Rules;
using Kodlama.io.Devs.Application.Features.ProgrammingLanguages.Rules;
using Kodlama.io.Devs.Application.Features.Technologies.Rules;
using Kodlama.io.Devs.Application.Services.Authorizations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kodlama.io.Devs.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddScoped<ProgrammingLanguageBusinessRules>();
            services.AddScoped<TechnologyBusinessRules>();
            services.AddScoped<AuthorizationBusinessRules>();
            services.AddScoped<GithubAddressBusinessRules>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CachingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(CacheRemovingBehavior<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehavior<,>));
            services.AddScoped<IAuthorizationService, AuthorizationManager>();
            services.AddTransient<ITokenHelper, JwtHelper>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            return services;

        }
    }
}