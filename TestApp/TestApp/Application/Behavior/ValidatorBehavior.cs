using FluentValidation;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TestApp.Application.Exceptions;

namespace TestApp.Application.Behavior
{
    public class ValidatorBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IValidator<TRequest>[] _validators;

        public ValidatorBehavior(IValidator<TRequest>[] validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var _failures = _validators.Select(v => v.Validate(request))
                 .SelectMany(result => result.Errors)
                 .Where(error => error != null)
                 .ToList();

            //How to deal with the validation response
            if (_failures.Any())
                throw new ValidationException(_failures);

            var response = await next();
            return response;
        }
    }
}
