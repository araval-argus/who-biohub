using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.UpdateTransportCategory;

public interface IUpdateTransportCategoryHandler
{
    Task<Either<UpdateTransportCategoryCommandResponse, Errors>> Handle(UpdateTransportCategoryCommand command, CancellationToken cancellationToken);
}

public class UpdateTransportCategoryHandler : IUpdateTransportCategoryHandler
{
    private readonly ILogger<UpdateTransportCategoryHandler> _logger;
    private readonly UpdateTransportCategoryCommandValidator _validator;
    private readonly IUpdateTransportCategoryMapper _mapper;
    private readonly ITransportCategoryWriteRepository _writeRepository;

    public UpdateTransportCategoryHandler(
        ILogger<UpdateTransportCategoryHandler> logger,
        UpdateTransportCategoryCommandValidator validator,
        IUpdateTransportCategoryMapper mapper,
        ITransportCategoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<UpdateTransportCategoryCommandResponse, Errors>> Handle(
        UpdateTransportCategoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        try
        {
            TransportCategory transportcategory = await _writeRepository.ReadForUpdate(command.Id, cancellationToken);
            transportcategory = _mapper.Map(transportcategory, command);

            Errors? errors = await _writeRepository.Update(transportcategory, cancellationToken);
            if (errors.HasValue)
                return new(errors.Value);

            return new(new UpdateTransportCategoryCommandResponse(transportcategory));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TransportCategory");
            throw;
        }
    }
}