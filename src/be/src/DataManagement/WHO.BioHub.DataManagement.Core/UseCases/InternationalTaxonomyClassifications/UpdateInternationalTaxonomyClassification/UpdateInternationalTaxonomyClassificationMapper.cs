using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.UpdateInternationalTaxonomyClassification;

public interface IUpdateInternationalTaxonomyClassificationMapper
{
    InternationalTaxonomyClassification Map(InternationalTaxonomyClassification internationaltaxonomyclassification, UpdateInternationalTaxonomyClassificationCommand command);
}

public class UpdateInternationalTaxonomyClassificationMapper : IUpdateInternationalTaxonomyClassificationMapper
{
    public InternationalTaxonomyClassification Map(InternationalTaxonomyClassification internationaltaxonomyclassification, UpdateInternationalTaxonomyClassificationCommand command)
    {        
        internationaltaxonomyclassification.Id = command.Id;
        internationaltaxonomyclassification.Name = command.Name;
        internationaltaxonomyclassification.Description = command.Description;
        internationaltaxonomyclassification.IsActive = command.IsActive;
        internationaltaxonomyclassification.DeletedOn = null;

        return internationaltaxonomyclassification;
    }
}