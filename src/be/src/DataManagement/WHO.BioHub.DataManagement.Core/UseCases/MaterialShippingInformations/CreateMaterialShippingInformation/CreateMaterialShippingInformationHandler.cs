using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.MaterialShippingInformations;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialShippingInformations.CreateMaterialShippingInformation;

public interface ICreateMaterialShippingInformationHandler
{
    Task<Either<CreateMaterialShippingInformationCommandResponse, Errors>> Handle(CreateMaterialShippingInformationCommand command, CancellationToken cancellationToken);
}

public class CreateMaterialShippingInformationHandler : ICreateMaterialShippingInformationHandler
{
    private readonly ILogger<CreateMaterialShippingInformationHandler> _logger;
    private readonly CreateMaterialShippingInformationCommandValidator _validator;
    private readonly ICreateMaterialShippingInformationMapper _mapper;
    private readonly IMaterialShippingInformationWriteRepository _writeRepository;

    public CreateMaterialShippingInformationHandler(
        ILogger<CreateMaterialShippingInformationHandler> logger,
        CreateMaterialShippingInformationCommandValidator validator,
        ICreateMaterialShippingInformationMapper mapper,
        IMaterialShippingInformationWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateMaterialShippingInformationCommandResponse, Errors>> Handle(
        CreateMaterialShippingInformationCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        MaterialShippingInformation materialshippinginformation = _mapper.Map(command);

        try
        {
            Either<MaterialShippingInformation, Errors> response = await _writeRepository.Create(materialshippinginformation, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            MaterialShippingInformation createdMaterialShippingInformation =
                response.Left ?? throw new Exception("This is a bug: materialshippinginformation value must be defined");
            return new(new CreateMaterialShippingInformationCommandResponse(response.Left));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new MaterialShippingInformation");
            throw;
        }
    }
}