using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.DataManagement.Core.UseCases.Laboratories.ListLaboratories;

public interface IListLaboratoryMapper
{
    IEnumerable<LaboratoryViewModel> Map(IEnumerable<Laboratory> laboratories);
    IEnumerable<LaboratoryViewModel> MapForLaboratoryUser(IEnumerable<Laboratory> laboratories);
}

public class ListLaboratoryMapper : IListLaboratoryMapper
{
    public IEnumerable<LaboratoryViewModel> Map(IEnumerable<Laboratory> laboratories)
    {
        List<LaboratoryViewModel> laboratoryViewModels = new List<LaboratoryViewModel>();

        foreach (var laboratory in laboratories)
        {
            LaboratoryViewModel laboratoryViewModel = new()
            {
                Id = laboratory.Id,
                Name = laboratory.Name,
                Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
                Address = laboratory.Address,
                Latitude = laboratory.Latitude.GetValueOrDefault(),
                Longitude = laboratory.Longitude.GetValueOrDefault(),
                CountryId = laboratory.CountryId,
                BSLLevelId = laboratory.BSLLevelId,
                IsPublicFacing = laboratory.IsPublicFacing,
                IsActive = laboratory.IsActive,
            };
            laboratoryViewModels.Add(laboratoryViewModel);
        }


        return laboratoryViewModels;
    }

    public IEnumerable<LaboratoryViewModel> MapForLaboratoryUser(IEnumerable<Laboratory> laboratories)
    {
        List<LaboratoryViewModel> laboratoryViewModels = new List<LaboratoryViewModel>();

        foreach (var laboratory in laboratories)
        {
            LaboratoryViewModel laboratoryViewModel = new()
            {
                Id = laboratory.Id,
                Name = laboratory.Name,
                Abbreviation = laboratory.Abbreviation != null ? laboratory.Abbreviation : string.Empty,
                Address = laboratory.Address,
                Latitude = laboratory.Latitude.GetValueOrDefault(),
                Longitude = laboratory.Longitude.GetValueOrDefault(),
                CountryId = laboratory.CountryId,                
                IsPublicFacing = laboratory.IsPublicFacing,
                IsActive = laboratory.IsActive,
            };
            laboratoryViewModels.Add(laboratoryViewModel);
        }


        return laboratoryViewModels;
    }
}