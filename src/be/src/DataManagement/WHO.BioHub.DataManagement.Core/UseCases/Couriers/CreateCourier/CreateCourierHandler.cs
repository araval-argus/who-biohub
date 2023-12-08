using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Couriers;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.Couriers.CreateCourier;

public interface ICreateCourierHandler
{
    Task<Either<CreateCourierCommandResponse, Errors>> Handle(CreateCourierCommand command, CancellationToken cancellationToken);
}

public class CreateCourierHandler : ICreateCourierHandler
{
    private readonly ILogger<CreateCourierHandler> _logger;
    private readonly CreateCourierCommandValidator _validator;
    private readonly ICreateCourierMapper _mapper;
    private readonly ICourierWriteRepository _writeRepository;

    public CreateCourierHandler(
        ILogger<CreateCourierHandler> logger,
        CreateCourierCommandValidator validator,
        ICreateCourierMapper mapper,
        ICourierWriteRepository writeRepository
    )
    {
        _logger = logger;
        _validator = validator;
        _mapper = mapper;
        _writeRepository = writeRepository;
    }

    public async Task<Either<CreateCourierCommandResponse, Errors>> Handle(
        CreateCourierCommand command,
        CancellationToken cancellationToken)
    {
        ValidationResult validationResult = await _validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
            return new(new Errors(validationResult));

        Courier courier = _mapper.Map(command);

        try
        {
            Either<Courier, Errors> response = await _writeRepository.Create(courier, cancellationToken);
            if (response.IsRight)
                return new(response.Right);

            Courier createdCourier =
                response.Left ?? throw new Exception("This is a bug: courier value must be defined");
            return new(new CreateCourierCommandResponse(response.Left.Id));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating a new Courier");
            throw;
        }
    }
}