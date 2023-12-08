using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Shipments;
using WHO.BioHub.DAL;
using WHO.BioHub.Public.SQL.Abstractions;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.Public.SQL.Repositories;

public class SQLShipmentPublicReadRepository : IShipmentPublicReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLShipmentPublicReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Shipment>> List(CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .ThenInclude(x => x.Country)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .ThenInclude(x => x.Country)
            .Where(l => l.DeletedOn == null)
            .Where(l => l.QELaboratory.DeletedOn == null && l.QELaboratory.IsPublicFacing == true && l.BioHubFacility.DeletedOn == null && l.BioHubFacility.IsPublicFacing == true)
            .Where(l =>
                (l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())
            ||
            (l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any()))
            .ToArrayAsync(cancellationToken);
    }

    public async Task<Shipment> Read(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Shipments
            .AsNoTracking()
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromBioHubFacility)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.RequestInitiationToLaboratory)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationToBioHubFacility)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.RequestInitiationFromLaboratory)
            .Include(x => x.WorklistFromBioHubItem)
            .ThenInclude(x => x.WorklistFromBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .Include(x => x.WorklistToBioHubItem)
            .ThenInclude(x => x.WorklistToBioHubItemMaterials)
            .ThenInclude(x => x.Material)
            .AsSplitQuery()
            .Where(l => l.QELaboratory.DeletedOn == null && l.QELaboratory.IsPublicFacing == true && l.BioHubFacility.DeletedOn == null && l.BioHubFacility.IsPublicFacing == true)
            .Where(l => l.DeletedOn == null &&
            l.Id == id &&
            ((l.WorklistFromBioHubItem != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials != null &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials.Any() &&
                l.WorklistFromBioHubItem.WorklistFromBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())
            ||
            (l.WorklistToBioHubItem != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials != null &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials.Any() &&
                l.WorklistToBioHubItem.WorklistToBioHubItemMaterials
                .Select(x => x.Material)
                .Where(x => x.PublicShare == YesNoOption.Yes && x.Status == MaterialStatus.Completed).Any())))
            .FirstOrDefaultAsync(cancellationToken);
    }


    public async Task<int> NumberOfIncomingShipments(CancellationToken cancellationToken)
    {
        //TODO: We consider into account the number of booking forms inside the shipment
        // No feedback received by the business
        return await _dbContext.BookingForms
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.WorklistToBioHubItemId != null)
            .Where(x => x.WorklistToBioHubItem.DeletedOn == null)
            .Where(x => x.WorklistToBioHubItem.Status == WorklistToBioHubStatus.ShipmentCompleted)
            .CountAsync(cancellationToken);

    }

    public async Task<int> NumberOfOutgoingShipments(CancellationToken cancellationToken)
    {
        //TODO: We consider into account the number of booking forms inside the shipment
        // No feedback received by the business
        return await _dbContext.BookingForms
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.WorklistFromBioHubItemId != null)
            .Where(x => x.WorklistFromBioHubItem.DeletedOn == null)            
            .Where(x => x.WorklistFromBioHubItem.Status == WorklistFromBioHubStatus.ShipmentCompleted)
            .CountAsync(cancellationToken);
    }


    public async Task<int> MaterialNumber(CancellationToken cancellationToken)
    {
        //TODO: We consider into account only the public materials
        // No feedback received by the business
        return await _dbContext.Materials
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.DeletedOn == null)
            .Where(x => x.Status == MaterialStatus.Completed)
            .Where(x => x.PublicShare == YesNoOption.Yes)
            .CountAsync(cancellationToken);
    }

    public async Task<int> CountryNumber(CancellationToken cancellationToken)
    {
        return await _dbContext.Laboratories
            .AsNoTracking()
            .AsSplitQuery()
            .Where(x => x.DeletedOn == null)
            .Select(x => x.CountryId)
            .Distinct()
            .CountAsync(cancellationToken);
    }


    public async Task<List<ShipmentsTypePercentage>> IncomingShipmentRate(CancellationToken cancellationToken)
    {

        var bookingForms = await _dbContext.BookingForms
            .AsNoTracking()
            .Include(x => x.TransportCategory)
            .AsSplitQuery()
            .Where(x => x.WorklistToBioHubItemId != null)
            .Where(x => x.WorklistToBioHubItem.DeletedOn == null)
            .Where(x => x.WorklistToBioHubItem.Status == WorklistToBioHubStatus.ShipmentCompleted)
            .ToListAsync(cancellationToken);


        var materialProductIds = bookingForms.Select(x => x.TransportCategoryId).Distinct();

        var shipmentTypesPercentages = new List<ShipmentsTypePercentage>();
        foreach (var materialProductId in materialProductIds)
        {
            ShipmentsTypePercentage shipmentTypesPercentage = new ShipmentsTypePercentage();
            shipmentTypesPercentage.Name = bookingForms.FirstOrDefault(x => x.TransportCategoryId == materialProductId).TransportCategory.Name;

            int totalTypeNumber = bookingForms.Where(x => x.TransportCategoryId == materialProductId).Count();
            int total = bookingForms.Count();

            shipmentTypesPercentage.Percentage = totalTypeNumber / total;
            shipmentTypesPercentages.Add(shipmentTypesPercentage);
        }

        return shipmentTypesPercentages;

    }


    public async Task<List<ShipmentsTypePercentage>> OutgoingShipmentRate(CancellationToken cancellationToken)
    {

        var bookingForms = await _dbContext.BookingForms
            .AsNoTracking()
            .Include(x => x.TransportCategory)
            .AsSplitQuery()
            .Where(x => x.WorklistFromBioHubItemId != null)
            .Where(x => x.WorklistFromBioHubItem.Status == WorklistFromBioHubStatus.ShipmentCompleted)
            .ToListAsync(cancellationToken);


        var materialProductIds = bookingForms.Select(x => x.TransportCategoryId).Distinct();

        var shipmentTypesPercentages = new List<ShipmentsTypePercentage>();
        foreach (var materialProductId in materialProductIds)
        {
            ShipmentsTypePercentage shipmentTypesPercentage = new ShipmentsTypePercentage();
            shipmentTypesPercentage.Name = bookingForms.FirstOrDefault(x => x.TransportCategoryId == materialProductId).TransportCategory.Name;

            int totalTypeNumber = bookingForms.Where(x => x.TransportCategoryId == materialProductId).Count();
            int total = bookingForms.Count();

            shipmentTypesPercentage.Percentage = totalTypeNumber / total;
            shipmentTypesPercentages.Add(shipmentTypesPercentage);
        }

        return shipmentTypesPercentages;

    }
}