using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.IsolationHostTypes.CreateIsolationHostType;

public record struct CreateIsolationHostTypeCommandResponse(IsolationHostType IsolationHostType) { }