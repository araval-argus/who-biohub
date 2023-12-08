using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ReadLaboratory;

public interface IReadLaboratoryMapper
{
    LaboratoryViewModel Map(Laboratory laboratory);
    LaboratoryViewModel MapForLaboratoryUser(Laboratory laboratory);
}

public class ReadLaboratoryMapper : IReadLaboratoryMapper
{
    public LaboratoryViewModel Map(Laboratory laboratory)
    {
        LaboratoryViewModel laboratoryViewModel = new()
        {
            Id = laboratory.Id,
            Name = laboratory.Name,
            Description = laboratory.Description,
            Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
            Address = laboratory.Address,
            Latitude = laboratory.Latitude.GetValueOrDefault(),
            Longitude = laboratory.Longitude.GetValueOrDefault(),
            CountryId = laboratory.CountryId,
            BSLLevelId = laboratory.BSLLevelId,
            IsActive = laboratory.IsActive,
            IsPublicFacing = laboratory.IsPublicFacing,
        };

        return laboratoryViewModel;
    }

    public LaboratoryViewModel MapForLaboratoryUser(Laboratory laboratory)
    {
        LaboratoryViewModel laboratoryViewModel = new()
        {
            Id = laboratory.Id,
            Name = laboratory.Name,
            Description = laboratory.Description,
            Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
            Address = laboratory.Address,
            Latitude = laboratory.Latitude.GetValueOrDefault(),
            Longitude = laboratory.Longitude.GetValueOrDefault(),
            CountryId = laboratory.CountryId,            
            IsActive = laboratory.IsActive,
            IsPublicFacing = laboratory.IsPublicFacing,
        };

        return laboratoryViewModel;
    }
}