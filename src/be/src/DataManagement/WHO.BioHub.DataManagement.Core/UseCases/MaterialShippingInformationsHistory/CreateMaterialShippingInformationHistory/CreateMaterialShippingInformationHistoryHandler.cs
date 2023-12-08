using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;

using WHO.BioHub.Models.Repositories.MaterialShippingInformationsHistory;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformationsHistory.CreateMaterialShippingInformationHistory;

public interface ICreateMaterialShippingInformationHistoryHandler
{
    Task<Either<CreateMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(CreateMaterialShippingInformationHistoryCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialShippingInformationHistoryHandler : ICreateMaterialShippingInformationHistoryHandler
{
    private readonly ILogger<CreateMaterialShippingInformationHistoryHandler> _logger;
    private readonly CreateMaterialShippingInformationHistoryCommandValidator _validator;
    private readonly ICreateMaterialShippingInformationHistoryMapper _mapper;
    private readonly IMaterialShippingInformationHistoryWriteRepository _writeRepository;

    public CreateMaterialShippingInformationHistoryHandler(
        ILogger<CreateMaterialShippingInformationHistoryHandler> logger,
        CreateMaterialShippingInformationHistoryCommandValidator validator,
        ICreateMaterialShippingInformationHistoryMapper mapper,
        IMaterialShippingInformationHistoryWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialShippingInformationHistoryCommandResponse, Errors>> Handle(
        CreateMaterialShippingInformationHistoryCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialShippingInformationHistory materialshippinginformationHistory = _mapper.Map(command);

        try
        {
            Either<MaterialShippingInformationHistory, Errors> response = await _writeRepository.Create(materialshippinginformationHistory, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialShippingInformationHistory createdMaterialShippingInformation =
                response.Left ?? throw new Exception("This is a bug: materialshippinginformation value must be defined");
            return new(new CreateMaterialShippingInformationHistoryCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialShippingInformation");
            throw;
        }
    }
}