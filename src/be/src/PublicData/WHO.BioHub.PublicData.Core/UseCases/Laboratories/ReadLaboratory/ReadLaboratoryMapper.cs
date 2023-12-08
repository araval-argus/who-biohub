using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ReadLaboratory;

public interface IReadLaboratoryMapper
{
    LaboratoryPublicViewModel Map(Laboratory laboratory);
}

public class ReadLaboratoryMapper : IReadLaboratoryMapper
{
    public LaboratoryPublicViewModel Map(Laboratory laboratory)
    {
        LaboratoryPublicViewModel laboratoryPublicViewModel = new()
        {
            Id = laboratory.Id,
            Name = laboratory.Name,
            Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
            Address = laboratory.Address,
            Latitude = laboratory.Latitude.GetValueOrDefault(),
            Longitude = laboratory.Longitude.GetValueOrDefault(),
            CountryId = laboratory.CountryId
        };

        return laboratoryPublicViewModel;
    }
}