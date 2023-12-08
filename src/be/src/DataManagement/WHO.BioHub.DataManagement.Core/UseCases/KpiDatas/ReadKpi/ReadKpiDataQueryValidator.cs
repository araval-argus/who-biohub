using FluentValidation;

namespace WHO.BioHub.Data.Core.UseCases.KpiDatas.ReadKpiData;

public class ReadKpiDataQueryValidator : AbstractValidator<ReadKpiDataQuery>
{
    public ReadKpiDataQueryValidator()
    {
        RuleFor(cmd => cmd);
    }
}