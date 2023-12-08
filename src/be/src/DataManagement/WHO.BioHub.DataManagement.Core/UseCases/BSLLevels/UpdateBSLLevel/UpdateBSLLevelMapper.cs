using WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;
using WHO.BioHub.Models.Models;

namespace WHO.BioHub.DataManagement.Core.UseCases.BSLLevels.UpdateBSLLevel;

public interface IUpdateBSLLevelMapper
{
    BSLLevel Map(BSLLevel bsllevel, UpdateBSLLevelCommand command);
}

public class UpdateBSLLevelMapper : IUpdateBSLLevelMapper
{
    public BSLLevel Map(BSLLevel bsllevel, UpdateBSLLevelCommand command)
    {       

        bsllevel.Id = command.Id;
        bsllevel.Code = command.Code;
        bsllevel.Name = command.Name;
        bsllevel.Description = command.Description;
        bsllevel.DeletedOn = null;

        return bsllevel;
    }
}