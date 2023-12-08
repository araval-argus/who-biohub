using WHO.BioHub.DAL;
using WHO.BioHub.Models.Repositories.Laboratories;

namespace WHO.BioHub.Search.SQL.Repositories.Entities;

partial class SQLLaboratorySearchRepository : ILaboratorySearchRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLLaboratorySearchRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }
}