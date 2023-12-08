using WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.CreateLaboratoryFromUserRequest;

public interface ICreateLaboratoryFromUserRequestMapper
{
    Laboratory Map(CreateLaboratoryFromUserRequestCommand command);
}

public class CreateLaboratoryFromUserRequestMapper : ICreateLaboratoryFromUserRequestMapper
{
    public Laboratory Map(CreateLaboratoryFromUserRequestCommand command)
    {
        Laboratory laboratory = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.InstituteName,
            CountryId = command.CountryId,
            IsActive = true,
            DeletedOn = null,
            LastOperationByUserId = command.OperationById,
            LastOperationDate = DateTime.UtcNow
        };

        return laboratory;
    }
}