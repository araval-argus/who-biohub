using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTARequests.ListSMTARequests;

public interface IListSMTARequestsMapper
{
    IEnumerable<SMTARequestViewModel> Map(IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems, IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems);
}

public class ListSMTARequestsMapper : IListSMTARequestsMapper
{
    public IEnumerable<SMTARequestViewModel> Map(IEnumerable<SMTA1WorkflowItem> SMTA1WorkflowItems, IEnumerable<SMTA2WorkflowItem> SMTA2WorkflowItems)
    {

        List<SMTARequestViewModel> list = new List<SMTARequestViewModel>();

        foreach (var SMTA1WorkflowItem in SMTA1WorkflowItems)
        {
            SMTARequestViewModel smtaViewModel = new SMTARequestViewModel();

            smtaViewModel.Id = SMTA1WorkflowItem.Id;
            smtaViewModel.SendBy = SMTA1WorkflowItem.LastOperationUser.FirstName + " " + SMTA1WorkflowItem.LastOperationUser.LastName;
            smtaViewModel.Institution = SMTA1WorkflowItem.Laboratory.Name;
            smtaViewModel.SMTAType = "SMTA 1";
            smtaViewModel.OperationDate = SMTA1WorkflowItem.OperationDate;
            smtaViewModel.WorkflowItemTitle = SMTA1WorkflowItem.WorkflowItemTitle;


            list.Add(smtaViewModel);
        }

        //foreach (var SMTA2WorkflowItem in SMTA2WorkflowItems)
        //{
        //    SMTARequestViewModel smtaViewModel = new SMTARequestViewModel();

        //    smtaViewModel.Id = SMTA2WorkflowItem.Id;
        //    smtaViewModel.SendBy = SMTA2WorkflowItem.LastOperationUser.FirstName + " " + SMTA2WorkflowItem.LastOperationUser.LastName;
        //    smtaViewModel.Institution = SMTA2WorkflowItem.RequestInitiationToLaboratory.Name;
        //    smtaViewModel.SMTAType = "SMTA 2";
        //    smtaViewModel.OperationDate = SMTA2WorkflowItem.OperationDate;
        //    smtaViewModel.WorklistItemTitle = SMTA2WorkflowItem.WorklistItemTitle;


        //    list.Add(smtaViewModel);
        //}

        list = list.OrderByDescending(x => x.OperationDate).ToList();

        return list;
    }



}