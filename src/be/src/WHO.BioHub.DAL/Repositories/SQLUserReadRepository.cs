using Microsoft.EntityFrameworkCore;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Models.Repositories.Users;
using WHO.BioHub.Shared.Enums;

namespace WHO.BioHub.DAL.Repositories;

public class SQLUserReadRepository : IUserReadRepository
{
    private readonly BioHubDbContext _dbContext;

    public SQLUserReadRepository(BioHubDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<User>> List(bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .Where(l => l.DeletedOn == null && l.CourierId == null);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }
        return await query
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListForLaboratoryUser(Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .Where(l => l.DeletedOn == null && l.LaboratoryId == laboratoryId);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }
        return await query
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListForBioHubFacilityUser(Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
             .Where(l => l.DeletedOn == null && l.BioHubFacilityId == bioHubFacilityId);


        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }
        return await query
            .ToArrayAsync(cancellationToken);
    }

    public async Task<User> Read(Guid id, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.Id == id && l.CourierId == null);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<User> ReadForLaboratoryUser(Guid id, Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.Id == id && l.LaboratoryId == laboratoryId);
        
        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<User> ReadForBioHubFacilityUser(Guid id, Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.Id == id && l.BioHubFacilityId == bioHubFacilityId);
        
        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }

        return await query.FirstOrDefaultAsync(cancellationToken);

    }

    public async Task<User> ReadCourierUser(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Id == id && l.CourierId != null, cancellationToken);
    }

    public async Task<IEnumerable<User>> ListUsersByLaboratoryId(Guid laboratoryId, bool excludeOnBehalfOf, CancellationToken cancellationToken)
    {
        var query = _dbContext.Users
            .Include(u => u.Laboratory)
            .ThenInclude(l => l.Country)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.LaboratoryId == laboratoryId);

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }

        return await query
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListUsersByBioHubFacilityId(Guid bioHubFacilityId, bool excludeOnBehalfOf, CancellationToken cancellationToken, bool? onlyFocalPoints = true)
    {
        var query = _dbContext.Users
            .Include(u => u.BioHubFacility)
            .ThenInclude(l => l.Country)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.BioHubFacilityId == bioHubFacilityId);

        if (onlyFocalPoints == true)
        {
            query = query.Where(x => x.OperationalFocalPoint == true);
        }

        if (excludeOnBehalfOf)
        {
            query = query.Where(x => x.Role.OnBehalfOf != true);
        }

        return await query
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListCourierUsers(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Where(l => l.DeletedOn == null && l.CourierId != null)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListCourierUsersForWorklist(CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(l => l.Courier)
            .ThenInclude(l => l.Country)
            .AsSplitQuery()
            .Where(l => l.DeletedOn == null && l.CourierId != null)
            .ToArrayAsync(cancellationToken);
    }

    public async Task<IEnumerable<User>> ListUsersByCourierId(Guid courierId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .Include(u => u.Laboratory)
            .ThenInclude(l => l.Country)
            .AsSplitQuery()
            .AsNoTracking()
            .Where(l => l.DeletedOn == null && l.CourierId == courierId)
            .ToListAsync(cancellationToken);
    }

    public async Task<User> ReadByExternalId(Guid externalId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.ExternalId == externalId, cancellationToken);
    }

    public async Task<User> ReadByEmail(string email, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Email == email, cancellationToken);
    }

    public async Task<User> ReadByEmailAuth(string email, Guid externalId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(l => l.DeletedOn == null && l.Email == email && (l.ExternalId == null || l.ExternalId == externalId), cancellationToken);
    }

    public async Task<bool> EmailPresent(string email, CancellationToken cancellationToken)
    {
        var presentAmongUsers = await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.Email == email, cancellationToken);

        if (presentAmongUsers)
        {
            return true;
        }

        return await _dbContext.UserRequests
            .AsNoTracking()
            .AnyAsync(l => l.DeletedOn == null && l.Email == email && l.Status == UserRegistrationStatus.Pending, cancellationToken);
       
    }

    public async Task<IEnumerable<User>> ListByPermissionName(string permissionName, Guid? laboratoryId, Guid? bioHubFacilityId, CancellationToken cancellationToken)
    {
        return await _dbContext.Users
             .Where(l => l.DeletedOn == null)
             .Where(l => l.Role.RolePermissions.Select(x => x.Permission.Name).Contains(permissionName))
             .Where(l => l.LaboratoryId == laboratoryId)
             .Where(l => l.BioHubFacilityId == bioHubFacilityId)
             .ToArrayAsync(cancellationToken);
    }

    public async Task<UserHistory> ReadPastInformation(Guid id, DateTime date, CancellationToken cancellationToken)
    {
        return await _dbContext.UsersHistory            
            .AsNoTracking()
            .AsSplitQuery()
            .Where(l => l.UserId == id && l.LastOperationDate <= date)
            .OrderByDescending(x => x.LastOperationDate)
            .FirstOrDefaultAsync(cancellationToken);
    }
}