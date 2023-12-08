using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.CreateWorklistToBioHubEmail;

public interface ICreateWorklistToBioHubEmailMapper
{
    WorklistToBioHubEmail Map(CreateWorklistToBioHubEmailCommand command);
}

public class CreateWorklistToBioHubEmailMapper : ICreateWorklistToBioHubEmailMapper
{
    public WorklistToBioHubEmail Map(CreateWorklistToBioHubEmailCommand command)
    {
        // TODO: Implement mapper

        WorklistToBioHubEmail worklisttobiohubemail = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,

            // ...

            DeletedOn = null,
        };

        return worklisttobiohubemail;
    }
}