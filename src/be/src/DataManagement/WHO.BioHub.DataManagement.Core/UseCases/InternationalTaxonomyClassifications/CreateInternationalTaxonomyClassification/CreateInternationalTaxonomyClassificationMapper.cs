using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.InternationalTaxonomyClassifications.CreateInternationalTaxonomyClassification;

public interface ICreateInternationalTaxonomyClassificationMapper
{
    InternationalTaxonomyClassification Map(CreateInternationalTaxonomyClassificationCommand command);
}

public class CreateInternationalTaxonomyClassificationMapper : ICreateInternationalTaxonomyClassificationMapper
{
    public InternationalTaxonomyClassification Map(CreateInternationalTaxonomyClassificationCommand command)
    {
       
        InternationalTaxonomyClassification internationaltaxonomyclassification = new()
        {
            Id = Guid.NewGuid(),
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            IsActive = command.IsActive,
            DeletedOn = null,
        };

        return internationaltaxonomyclassification;
    }
}