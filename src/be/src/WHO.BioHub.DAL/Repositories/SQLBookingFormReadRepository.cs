using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.BookingForms;

namespace WHO.BioHub.DAL.Repositories;

public class SQLBookingFormReadRepository : IBookingFormReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLBookingFormReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<BookingForm>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.BookingForms
            .Where(l => l.DeletedOn == null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<BookingForm>> ListByCourierIdWithExtraInformation(Guid courierId, CancellationToken cancellationToken)
    {

        return await _dbContext.BookingForms
            .AsNoTracking()
            .Include(x => x.WorklistToBioHubItem)
            .Include(x => x.WorklistFromBioHubItem)
            .Where(l => l.DeletedOn == null && l.CourierId == courierId).ToArrayAsync(cancellationToken);

    }

    public async Task<BookingForm> ReadByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var bookingFormInfo = await _dbContext.BookingForms.AsNoTracking().Where(x => x.Id == id).Select(x => new { x.WorklistFromBioHubItemId, x.WorklistToBioHubItemId }).FirstOrDefaultAsync(cancellationToken);
        if (bookingFormInfo.WorklistToBioHubItemId != null)
        {
            return await ReadToBioHubByIdWithExtraInfo(id, cancellationToken);
        }
        else if (bookingFormInfo.WorklistFromBioHubItemId != null)
        {
            return await ReadFromBioHubByIdWithExtraInfo(id, cancellationToken);
        }
        return null;

    }


    public async Task<BookingForm> ReadFromBioHubByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {

        var query = _dbContext.BookingForms.AsNoTracking();

        query = query
                    .Include(l => l.WorklistFromBioHubItem)
                    .ThenInclude(x => x.WorklistFromBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(l => l.WorklistFromBioHubItem)
                    .ThenInclude(l => l.WorklistFromBioHubItemMaterials)
                    .ThenInclude(l => l.Material)
                    .Include(l => l.WorklistFromBioHubItem)
                    .ThenInclude(l => l.WorklistFromBioHubHistoryItems)
                    .ThenInclude(l => l.LastOperationUser)
                    .Include(l => l.WorklistFromBioHubItem)
                    .ThenInclude(l => l.RequestInitiationToLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(l => l.WorklistFromBioHubItem)
                    .ThenInclude(l => l.RequestInitiationFromBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.TransportCategory)
                    .Include(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.BioHubFacility)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.TransportMode); 


        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }


    public async Task<BookingForm> ReadToBioHubByIdWithExtraInfo(Guid id, CancellationToken cancellationToken)
    {
        var query = _dbContext.BookingForms.AsNoTracking();

        query = query            
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(x => x.WorklistToBioHubItemLaboratoryFocalPoints)
                    .ThenInclude(x => x.User)              
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(l => l.MaterialShippingInformations)                   
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(l => l.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Users)
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(l => l.WorklistToBioHubHistoryItems)
                    .ThenInclude(l => l.LastOperationUser)
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(l => l.RequestInitiationFromLaboratory)
                    .ThenInclude(l => l.Country)
                    .Include(l => l.WorklistToBioHubItem)
                    .ThenInclude(l => l.RequestInitiationToBioHubFacility)
                    .ThenInclude(l => l.Country)
                    .Include(x => x.TransportCategory)
                    .Include(x => x.BookingFormPickupUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Laboratory)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.BookingFormCourierUsers)
                    .ThenInclude(x => x.User)
                    .ThenInclude(x => x.Courier)
                    .ThenInclude(x => x.Country)
                    .Include(x => x.TransportMode);

        query = query.AsSplitQuery();

        return await query.FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);

    }

    public async Task<BookingForm> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.BookingForms
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id, cancellationToken);
    }


}