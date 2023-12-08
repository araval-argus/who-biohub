using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubEmails.UpdateWorklistToBioHubEmail;

public interface IUpdateWorklistToBioHubEmailMapper
{
    WorklistToBioHubEmail Map(WorklistToBioHubEmail worklisttobiohubemail, UpdateWorklistToBioHubEmailCommand command);
}

public class UpdateWorklistToBioHubEmailMapper : IUpdateWorklistToBioHubEmailMapper
{
    public WorklistToBioHubEmail Map(WorklistToBioHubEmail worklisttobiohubemail, UpdateWorklistToBioHubEmailCommand command)
    {
        // TODO: Implement mapper

        worklisttobiohubemail.Id = command.Id;
        worklisttobiohubemail.CreationDate = DateTime.UtcNow;

        // ...

        worklisttobiohubemail.DeletedOn = null;

        return worklisttobiohubemail;
    }
}