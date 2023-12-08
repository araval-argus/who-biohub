using FluentValidation;

namespace WHO.BioHub.PublicData.Core.UseCases.KpiDatas.ReadKpiData;

public class ReadKpiDataQueryValidator : AbstractValidator<ReadKpiDataQuery>
{
    public ReadKpiDataQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}