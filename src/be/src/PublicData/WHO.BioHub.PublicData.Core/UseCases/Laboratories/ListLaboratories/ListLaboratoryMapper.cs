using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.Laboratories.ListLaboratories;

public interface IListLaboratoryMapper
{
    IEnumerable<LaboratoryPublicViewModel> Map(IEnumerable<Laboratory> laboratories);
}

public class ListLaboratoryMapper : IListLaboratoryMapper
{
    public IEnumerable<LaboratoryPublicViewModel> Map(IEnumerable<Laboratory> laboratories)
    {
        List<LaboratoryPublicViewModel> laboratoryPublicViewModels = new List<LaboratoryPublicViewModel>();

        foreach (var laboratory in laboratories)
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
            laboratoryPublicViewModels.Add(laboratoryPublicViewModel);
        }


        return laboratoryPublicViewModels;
    }
}