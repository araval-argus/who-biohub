using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Resources;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Resources.UpdateResource;

public interface IUpdateResourceHandler
{
    Task<Either<UpdateResourceCommandResponse, Errors>> Handle(UpdateResourceCommand command, CancellationToken cancellationToken);
}

public class UpdateResourceHandler : IUpdateResourceHandler
{
    private readonly ILogger<UpdateResourceHandler> _logger;
    private readonly UpdateResourceCommandValidator _validator;
    private readonly IUpdateResourceMapper _mapper;
    private readonly IResourceWriteRepository _writeRepository;

    public UpdateResourceHandler(
        ILogger<UpdateResourceHandler> logger,
        UpdateResourceCommandValidator validator,
        IUpdateResourceMapper mapper,
        IResourceWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateResourceCommandResponse, Errors>> Handle(
        UpdateResourceCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            return GetValidationMessages(validationResult);
        }

        try
        {
            Resource resource = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            if (resource == null)
                return new(new Errors(ErrorType.NotFound, $"Resource with Id {command.Id} not found"));

            resource.Name = command.Name;
         
            Errors? errors = await _writeRepository.Update(resource, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);           

            return new(new UpdateResourceCommandResponse(resource.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Resource");
            throw;
        }
    }

    private Either<UpdateResourceCommandResponse, Errors> GetValidationMessages(ValidationResult validationResult)
    {
        List<string> errors = new List<string>();
        foreach (ValidationFailure? error in validationResult.Errors)
        {
            errors.Add(error.ErrorMessage);
        }
        return new(new Errors(ErrorType.RequestParsing, errors.ToArray()));
    }
}