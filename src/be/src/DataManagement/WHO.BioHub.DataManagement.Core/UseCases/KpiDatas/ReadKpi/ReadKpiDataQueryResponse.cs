using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
namespace WHO.BioHub.Data.Core.UseCases.KpiDatas.ReadKpiData;

public record struct ReadKpiDataQueryResponse(KpiViewModel Kpi) { }