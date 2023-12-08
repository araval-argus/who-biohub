using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.UserRequests;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.UserRequests.ReadUserRequest;

public interface IReadUserRequestHandler
{
    Task<Either<ReadUserRequestQueryResponse, Errors>> Handle(ReadUserRequestQuery query, CancellationToken cancellationToken);
}

public class ReadUserRequestHandler : IReadUserRequestHandler
{
    private readonly ILogger<ReadUserRequestHandler> _logger;
    private readonly ReadUserRequestQueryValidator _validator;
    private readonly IUserRequestReadRepository _readRepository;

    private readonly IReadUserRequestMapper _mapper;

    public ReadUserRequestHandler(
        ILogger<ReadUserRequestHandler> logger,
        ReadUserRequestQueryValidator validator,
        IUserRequestReadRepository readRepository,
        IReadUserRequestMapper mapper)
    {
        _logger = logger;
        _validator = validator;
        _readRepository = readRepository;
        _mapper = mapper;
    }

    public async Task<Either<ReadUserRequestQueryResponse, Errors>> Handle(
        ReadUserRequestQuery query,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            UserRequest userRequest = await _readRepository.Read(query.Id, cancellationToken);
            if (userRequest == null)
                return new(new Errors(ErrorType.NotFound, $"UserRequest with Id {query.Id} not found"));

            var userRequestViewModel = _mapper.Map(userRequest);

            return new(new ReadUserRequestQueryResponse(userRequestViewModel));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error reading UserRequest with Id {id}", query.Id);
            throw;
        }
    }
}