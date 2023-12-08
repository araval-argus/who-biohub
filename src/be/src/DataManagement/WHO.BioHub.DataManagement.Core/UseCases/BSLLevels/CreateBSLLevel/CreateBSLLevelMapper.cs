using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.CreateBSLLevel;

public interface ICreateBSLLevelMapper
{
    BSLLevel Map(CreateBSLLevelCommand command);
}

public class CreateBSLLevelMapper : ICreateBSLLevelMapper
{
    public BSLLevel Map(CreateBSLLevelCommand command)
    {
       
        BSLLevel bsllevel = new()
        {
            Id = Guid.NewGuid(),
            Code = command.Code,
            CreationDate = DateTime.UtcNow,
            Name = command.Name,
            Description = command.Description,
            DeletedOn = null,
        };

        return bsllevel;
    }
}