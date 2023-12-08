using WHO.BioHub.Models.Models;
using WHO.BioHub.PublicData.Core.Dto;
using WHO.BioHub.Shared.Dto;

namespace WHO.BioHub.PublicData.Core.UseCases.KpiDatas.ReadKpiData;

public record struct ReadKpiDataQueryResponse(PublicKpiViewModel Kpi) { }