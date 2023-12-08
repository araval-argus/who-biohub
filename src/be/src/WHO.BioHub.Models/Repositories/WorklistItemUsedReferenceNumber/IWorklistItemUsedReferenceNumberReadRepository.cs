namespace WHO.BioHub.Models.Repositories.WorklistItemUsedReferenceNumber
{
    public interface IWorklistItemUsedReferenceNumberReadRepository
    {
        Task<int> Count(bool? isPast, CancellationToken cancellationToken);
        Task<bool> ReferenceNumberAlreadyPresent(string referenceNumber, CancellationToken cancellationToken);
    }
}


