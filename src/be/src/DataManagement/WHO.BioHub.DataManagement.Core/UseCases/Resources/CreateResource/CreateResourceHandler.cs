using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.CreateResource;

public interface ICreateResourceHandler
{
    Task<Either<CreateResourceCommandResponse, Errors>> Handle(CreateResourceCommand command, CancellationToken cancellationToken);
}

public class CreateResourceHandler : ICreateResourceHandler
{
    private readonly ILogger<CreateResourceHandler> _logger;
    private readonly CreateResourceCommandValidator _validator;
    private readonly ICreateResourceMapper _mapper;
    private readonly IResourceWriteRepository _writeRepository;

    public CreateResourceHandler(
        ILogger<CreateResourceHandler> logger,
        CreateResourceCommandValidator validator,
        ICreateResourceMapper mapper,
        IResourceWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateResourceCommandResponse, Errors>> Handle(
        CreateResourceCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Resource resource = _mapper.Map(command);

        try
        {
            Either<Resource, Errors> response = await _writeRepository.Create(resource, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Resource createdResource =
                response.Left ?? throw new Exception("This is a bug: resource value must be defined");
            return new(new CreateResourceCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Resource");
            throw;
        }
    }
}