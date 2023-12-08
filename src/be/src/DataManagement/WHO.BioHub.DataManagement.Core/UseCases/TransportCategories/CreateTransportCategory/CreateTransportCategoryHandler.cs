using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.TransportCategories;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.TransportCategories.CreateTransportCategory;

public interface ICreateTransportCategoryHandler
{
    Task<Either<CreateTransportCategoryCommandResponse, Errors>> Handle(CreateTransportCategoryCommand command, CancellationToken cancellationToken);
}

public class CreateTransportCategoryHandler : ICreateTransportCategoryHandler
{
    private readonly ILogger<CreateTransportCategoryHandler> _logger;
    private readonly CreateTransportCategoryCommandValidator _validator;
    private readonly ICreateTransportCategoryMapper _mapper;
    private readonly ITransportCategoryWriteRepository _writeRepository;

    public CreateTransportCategoryHandler(
        ILogger<CreateTransportCategoryHandler> logger,
        CreateTransportCategoryCommandValidator validator,
        ICreateTransportCategoryMapper mapper,
        ITransportCategoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateTransportCategoryCommandResponse, Errors>> Handle(
        CreateTransportCategoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        TransportCategory transportcategory = _mapper.Map(command);

        try
        {
            Either<TransportCategory, Errors> response = await _writeRepository.Create(transportcategory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            TransportCategory createdTransportCategory =
                response.Left ?? throw new Exception("This is a bug: transportcategory value must be defined");
            return new(new CreateTransportCategoryCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new TransportCategory");
            throw;
        }
    }
}