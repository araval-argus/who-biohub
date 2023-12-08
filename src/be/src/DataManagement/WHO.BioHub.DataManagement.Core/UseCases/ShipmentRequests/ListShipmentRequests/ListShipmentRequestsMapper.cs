using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Permissions;
using WHO.BioHub.Shared.Utils;

namespace WHO.BioHub.DataManagement.Core.UseCases.ShipmentRequests.ListShipmentRequests;

public interface IListShipmentRequestsMapper
{
    IEnumerable<ShipmentRequestViewModel> Map(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems, IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems);
}

public class ListShipmentRequestsMapper : IListShipmentRequestsMapper
{
    public IEnumerable<ShipmentRequestViewModel> Map(IEnumerable<WorklistToBioHubItem> worklistToBioHubItems, IEnumerable<WorklistFromBioHubItem> worklistFromBioHubItems)
    {        

        List<ShipmentRequestViewModel> list = new List<ShipmentRequestViewModel>();

        foreach (var worklistToBioHubItem in worklistToBioHubItems)
        {
            ShipmentRequestViewModel shipmentViewModel = new ShipmentRequestViewModel();
            shipmentViewModel.Id = worklistToBioHubItem.Id;
            shipmentViewModel.SendBy = worklistToBioHubItem.LastOperationUser.FirstName + " " + worklistToBioHubItem.LastOperationUser.LastName;
            shipmentViewModel.Institution = worklistToBioHubItem.RequestInitiationFromLaboratory.Name;
            shipmentViewModel.ShipmentDirection = Shared.Enums.ShipmentDirection.ToBioHub;
            shipmentViewModel.OperationDate = worklistToBioHubItem.OperationDate;
            shipmentViewModel.WorklistItemTitle = worklistToBioHubItem.WorklistItemTitle;

            shipmentViewModel.CurrentStatusName = worklistToBioHubItem.LastSubmissionApproved != false ? worklistToBioHubItem.Status.StatusName() : worklistToBioHubItem.Status.WorklistItemRejectedTitle();
            shipmentViewModel.LaboratoryCountryIso = worklistToBioHubItem.RequestInitiationFromLaboratory.Country.Iso3;

            list.Add(shipmentViewModel);
        }

        foreach (var worklistFromBioHubItem in worklistFromBioHubItems)
        {
            ShipmentRequestViewModel shipmentViewModel = new ShipmentRequestViewModel();

            shipmentViewModel.Id = worklistFromBioHubItem.Id;
            shipmentViewModel.SendBy = worklistFromBioHubItem.LastOperationUser.FirstName + " " + worklistFromBioHubItem.LastOperationUser.LastName;
            shipmentViewModel.Institution = worklistFromBioHubItem.RequestInitiationToLaboratory.Name;
            shipmentViewModel.ShipmentDirection = Shared.Enums.ShipmentDirection.FromBioHub;
            shipmentViewModel.OperationDate = worklistFromBioHubItem.OperationDate;
            shipmentViewModel.WorklistItemTitle = worklistFromBioHubItem.WorklistItemTitle;

            shipmentViewModel.CurrentStatusName = worklistFromBioHubItem.LastSubmissionApproved != false ? worklistFromBioHubItem.Status.StatusName() : worklistFromBioHubItem.Status.WorklistItemRejectedTitle();
            shipmentViewModel.LaboratoryCountryIso = worklistFromBioHubItem.RequestInitiationToLaboratory.Country.Iso3;


            list.Add(shipmentViewModel);
        }

        list = list.OrderByDescending(x => x.OperationDate).ToList();

        return list;
    }



}