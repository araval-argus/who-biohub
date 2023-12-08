using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DataManagement.Core.UseCases.AuthChecks.GetAccessInformation;

public record struct GetAccessInformationQueryResponse(
    UserLoginInfo UserLoginInfo
    );