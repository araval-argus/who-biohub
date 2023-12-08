using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using System.Globalization;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.DataManagement.Core.UseCases.WorklistToBioHubItems.ListWorklistToBioHubEventItem;

public interface IListWorklistToBioHubItemEventMapper
{
    List<WorklistTimeline> Map(WorklistToBioHubItem entity);
}

public class ListWorklistToBioHubItemEventMapper : IListWorklistToBioHubItemEventMapper
{

    public List<WorklistTimeline> Map(WorklistToBioHubItem entity)
    {
        List<WorklistTimeline> overallList = new List<WorklistTimeline>();

        if (entity.BookingForms != null && entity.BookingForms.Any())
        {
            foreach (var bookingForm in entity.BookingForms)
            {
                WorklistTimeline worklistTimeline = new WorklistTimeline();                

                worklistTimeline.Events = GetList(entity, bookingForm);

                worklistTimeline.TimelineTitle = bookingForm.TransportCategory?.Name ?? string.Empty;

                overallList.Add(worklistTimeline);
            }

            overallList = overallList.OrderBy(x => x.TimelineTitle).ToList();
        }

        else
        {

            WorklistTimeline worklistTimeline = new WorklistTimeline();

            worklistTimeline.Events = GetList(entity);

            worklistTimeline.TimelineTitle = string.Empty;

            overallList.Add(worklistTimeline);
        }

        return overallList;
    }

    private List<WorklistTimelineEventsDayDto> GetList(WorklistToBioHubItem entity, BookingForm? bookingForm = null)
    {
        List<WorklistTimelineEventsDayDto> list = new List<WorklistTimelineEventsDayDto>();

        DateTime shipmentRequestDate = entity.WorklistToBioHubHistoryItems.Any() ? (entity.WorklistToBioHubHistoryItems.OrderBy(x => x.OperationDate).FirstOrDefault(x => x.Status == WorklistToBioHubStatus.SubmitAnnex2OfSMTA1)?.OperationDate).GetValueOrDefault() : entity.OperationDate.GetValueOrDefault();


        AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ShipmentRequest, 0, shipmentRequestDate, WorklistEventStageNames.PreShipment, WorklistEventType.Milestone);

        DateTime? approvedAnnex2Date = GetApprovedEventDate(entity, WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval);

        if (approvedAnnex2Date != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ApprovedAnnex2OfSMTA1, (approvedAnnex2Date.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, approvedAnnex2Date.GetValueOrDefault(), WorklistEventStageNames.PreShipment, WorklistEventType.Milestone);
        }

        DateTime? approvedBookingFormDate = GetApprovedEventDate(entity, WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval);

        if (approvedBookingFormDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ApprovedBookingFormOfSMTA1, (approvedBookingFormDate.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, approvedBookingFormDate.GetValueOrDefault(), WorklistEventStageNames.PreShipment, WorklistEventType.Milestone);
        }

        var submitAnnex2Dates = GetMinorEventDates(entity, WorklistToBioHubStatus.SubmitAnnex2OfSMTA1, true);

        if (submitAnnex2Dates != null && submitAnnex2Dates.Any())
        {
            var firstSubmit = submitAnnex2Dates.OrderBy(x => x.Date).FirstOrDefault();
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.SubmitAnnex2OfSMTA1, (firstSubmit.Date - shipmentRequestDate.Date).Days, firstSubmit, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);

            foreach (var submitAnnex2Date in submitAnnex2Dates)
            {
                if (firstSubmit != submitAnnex2Date)
                {
                    AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReSubmitAnnex2OfSMTA1, (submitAnnex2Date.Date - shipmentRequestDate.Date).Days, submitAnnex2Date, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);
                }
            }
        }

        var rejectAnnex2Dates = GetMinorEventDates(entity, WorklistToBioHubStatus.WaitingForAnnex2OfSMTA1SECsApproval, false);

        if (rejectAnnex2Dates != null && rejectAnnex2Dates.Any())
        {
            foreach (var rejectAnnex2Date in rejectAnnex2Dates)
            {
                AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReturnedAnnex2OfSMTA1, (rejectAnnex2Date.Date - shipmentRequestDate.Date).Days, rejectAnnex2Date, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);
            }
        }


        var submitBookingFormDates = GetMinorEventDates(entity, WorklistToBioHubStatus.SubmitBookingFormOfSMTA1, true);

        if (submitBookingFormDates != null && submitBookingFormDates.Any())
        {
            var firstSubmit = submitBookingFormDates.OrderBy(x => x.Date).FirstOrDefault();
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.SubmitBookingFormOfSMTA1, (firstSubmit.Date - shipmentRequestDate.Date).Days, firstSubmit, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);


            foreach (var submitBookingFormDate in submitBookingFormDates)
            {
                if (firstSubmit != submitBookingFormDate)
                {
                    AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReSubmitBookingFormOfSMTA1, (submitBookingFormDate.Date - shipmentRequestDate.Date).Days, submitBookingFormDate, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);
                }
            }
        }

        var rejectBookingFormDates = GetMinorEventDates(entity, WorklistToBioHubStatus.WaitForBookingFormSMTA1OPSApproval, false);

        if (rejectBookingFormDates != null && rejectBookingFormDates.Any())
        {
            foreach (var rejectBookingFormDate in rejectBookingFormDates)
            {
                AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReturnedBookingFormOfSMTA1, (rejectBookingFormDate.Date - shipmentRequestDate.Date).Days, rejectBookingFormDate, WorklistEventStageNames.PreShipment, WorklistEventType.Minor);
            }
        }

        if (bookingForm != null)
        {
            if (bookingForm.DateOfPickup != null)
            {
                AddOrUpdateTimelineEventsDays(list, WorklistEventNames.PickupDate, (bookingForm.DateOfPickup.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, bookingForm.DateOfPickup.GetValueOrDefault(), WorklistEventStageNames.Shipment, WorklistEventType.Minor);
            }

            if (bookingForm.DateOfDelivery != null)
            {
                AddOrUpdateTimelineEventsDays(list, WorklistEventNames.DeliveryDate, (bookingForm.DateOfDelivery.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, bookingForm.DateOfDelivery.GetValueOrDefault(), WorklistEventStageNames.Shipment, WorklistEventType.Minor);
            }
        }


        DateTime? pickupCompletedDate = GetApprovedEventDate(entity, WorklistToBioHubStatus.WaitForPickUpCompleted);

        if (pickupCompletedDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.PickupCompleted, (pickupCompletedDate.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, pickupCompletedDate.GetValueOrDefault(), WorklistEventStageNames.Shipment, WorklistEventType.Milestone);
        }

        DateTime? deliveryCompletedDate = GetApprovedEventDate(entity, WorklistToBioHubStatus.WaitForDeliveryCompleted);

        if (deliveryCompletedDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.DeliveryCompleted, (deliveryCompletedDate.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, deliveryCompletedDate.GetValueOrDefault(), WorklistEventStageNames.Shipment, WorklistEventType.Milestone);
        }

        DateTime? shipmentCompletedDate = entity.Status == WorklistToBioHubStatus.ShipmentCompleted ? entity.OperationDate.GetValueOrDefault().Date : null;

        if (shipmentCompletedDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ShipmentCompleted, (shipmentCompletedDate.GetValueOrDefault().Date - shipmentRequestDate.Date).Days, shipmentCompletedDate.GetValueOrDefault(), WorklistEventStageNames.PostShipment, WorklistEventType.Milestone);
        };

        list = list.OrderBy(x => x.Date).ToList();

        if (list.Any())
        {

            DateTime? lowerDate = list.FirstOrDefault()?.Date;

            DateTime? higherDate = list.LastOrDefault()?.Date;

            DateTime firstDate = lowerDate.Value.AddDays(-7);

            DateTime lastDate = higherDate.Value.AddDays(3);

            //if ((lastDate - lowerDate.GetValueOrDefault()).Days < 31)
            //{
            //    lastDate = lastDate.AddDays(31 - (lastDate - lowerDate.GetValueOrDefault()).Days);
            //}

            if (firstDate != lastDate)
            {
                for (var date = firstDate; date < lastDate; date = date.AddDays(1))
                {
                    if (!list.Any(x => x.Date.Date == date.Date))
                    {
                        AddOrUpdateTimelineEventsDays(list, string.Empty, (date.Date - shipmentRequestDate.Date).Days, date, string.Empty, WorklistEventType.None);
                    }
                }
            }
        }
        list = list.OrderBy(x => x.Date).ToList();
        SetPositions(list);
        return list;
    }

    private void SetPositions(List<WorklistTimelineEventsDayDto> list)
    {
        string position = WorklistEventPositionNames.Up;

        foreach (var elem in list)
        {
            foreach (var dayEvent in elem.WorklistTimelineEventsTypes)
            {
                dayEvent.Position = position;
                position = ChangePosition(position);
            }
        }

    }

    private string ChangePosition(string position)
    {
        if (position == WorklistEventPositionNames.Up)
        {
            position = WorklistEventPositionNames.Down;
        }
        else
        {
            position = WorklistEventPositionNames.Up;
        }
        return position;
    }


    private DateTime? GetApprovedEventDate(WorklistToBioHubItem entity, WorklistToBioHubStatus previousStatus)
    {
        if (entity.PreviousStatus == previousStatus && entity.LastSubmissionApproved == true)
        {
            return entity.OperationDate != null ? entity.OperationDate.GetValueOrDefault() : null;
        }
        else
        {
            if (entity.WorklistToBioHubHistoryItems == null)
            {
                return null;
            }

            var historyEntity = entity.WorklistToBioHubHistoryItems.Where(x => x.PreviousStatus == previousStatus && x.LastSubmissionApproved == true).FirstOrDefault();

            if (historyEntity != null)
            {
                return historyEntity.OperationDate != null ? historyEntity.OperationDate.GetValueOrDefault() : null;
            }
        }

        return null;
    }


    private List<DateTime> GetMinorEventDates(WorklistToBioHubItem entity, WorklistToBioHubStatus previousStatus, bool lastSubmissionApproved)
    {
        var list = new List<DateTime>();
        if (entity.PreviousStatus == previousStatus && entity.LastSubmissionApproved == lastSubmissionApproved)
        {
            if (entity.OperationDate != null)
            {
                list.Add(entity.OperationDate.GetValueOrDefault());
            }
        }

        if (entity.WorklistToBioHubHistoryItems != null && entity.WorklistToBioHubHistoryItems.Any())
        {
            var historyEntityList = entity.WorklistToBioHubHistoryItems.Where(x => x.PreviousStatus == previousStatus && x.LastSubmissionApproved == lastSubmissionApproved).ToList();

            if (historyEntityList != null && historyEntityList.Any())
            {
                list.AddRange(historyEntityList.Select(x => x.OperationDate.GetValueOrDefault()).ToList());
            }
        }

        return list;
    }

    private void AddOrUpdateTimelineEventsDays(
        List<WorklistTimelineEventsDayDto> worklistTimelineEventDays,
        string label,
        int dateZeroBased,
        DateTime dateTime,        
        string stage,
        WorklistEventType eventType        
    )
    {
        var existingElement = worklistTimelineEventDays.Where(x => x.Date.Date == dateTime.Date).FirstOrDefault();

        if (existingElement == null)
        {
            WorklistTimelineEventsDayDto worklistTimelineDayEvents = new WorklistTimelineEventsDayDto();
            worklistTimelineDayEvents.WorklistTimelineEventsTypes = new List<WorklistTimelineEventsTypeDto>();
            worklistTimelineDayEvents.DateZeroBased = dateZeroBased;
            worklistTimelineDayEvents.Date = dateTime.Date;
            worklistTimelineDayEvents.DateString = dateTime.Date.Day.ToString("D2") + " " + dateTime.Date.ToString("MMM", CultureInfo.InvariantCulture);
            worklistTimelineDayEvents.DateDD = dateTime.Date.Day.ToString("D2");
            worklistTimelineDayEvents.DateMMM = dateTime.Date.ToString("MMM", CultureInfo.InvariantCulture);
            worklistTimelineDayEvents.DateMM = dateTime.Date.Month.ToString("D2");
            worklistTimelineDayEvents.DateYYYY = dateTime.Date.Year.ToString("D4");
            worklistTimelineDayEvents.Stage = stage;

            if (eventType != WorklistEventType.None)
            {
                AddWorklistTimelineEventsType(label, dateTime, stage, eventType, worklistTimelineDayEvents);
            }

            worklistTimelineEventDays.Add(worklistTimelineDayEvents);
           
        }
        else
        {
            if (existingElement.WorklistTimelineEventsTypes.Any(x => x.EventType == eventType))
            {
                WorklistTimelineEventsTypeDto worklistTimelineEventsType = existingElement.WorklistTimelineEventsTypes.FirstOrDefault(x => x.EventType == eventType);

                worklistTimelineEventsType.EventsNumber++;
                worklistTimelineEventsType.Label = $"{worklistTimelineEventsType.EventsNumber} Events";

                AddWorklistEventDetailItem(label, dateTime, worklistTimelineEventsType);
            }

            else
            {                

                AddWorklistTimelineEventsType(label, dateTime, stage, eventType, existingElement);                        
                
            }          

        }
        

    }

    private void AddWorklistTimelineEventsType(string label, DateTime dateTime, string stage, WorklistEventType eventType, WorklistTimelineEventsDayDto worklistTimelineDayEventsDto)
    {
        WorklistTimelineEventsTypeDto worklistTimelineEventsType = new WorklistTimelineEventsTypeDto();
        worklistTimelineEventsType.Id = Guid.NewGuid();
        worklistTimelineEventsType.Label = label;             
        worklistTimelineEventsType.EventsNumber = 1;
        worklistTimelineEventsType.EventType = eventType;
        worklistTimelineEventsType.Title = eventType == WorklistEventType.Milestone ? "Key Milestone on" : "Milestone on";
        worklistTimelineEventsType.Date = dateTime.Day.ToString("D2") + " " + ToMonthName(dateTime) + " " + dateTime.Year.ToString("D4");


        AddWorklistEventDetailItem(label, dateTime, worklistTimelineEventsType);

        worklistTimelineDayEventsDto.WorklistTimelineEventsTypes.Add(worklistTimelineEventsType);
    }    

    private void AddWorklistEventDetailItem(string label, DateTime? eventDateTime, WorklistTimelineEventsTypeDto worklistTimelineEventsTypeDto)
    {
        WorklistEventDetailItemDto eventDetailItemDto = new WorklistEventDetailItemDto();
        eventDetailItemDto.Time = eventDateTime.Value.Hour.ToString("D2") + ":" + eventDateTime.Value.Minute.ToString("D2");
        eventDetailItemDto.Event = label;
        eventDetailItemDto.EventDateTime = eventDateTime;

        if (worklistTimelineEventsTypeDto.EventDetailItems == null)
        {
            worklistTimelineEventsTypeDto.EventDetailItems = new List<WorklistEventDetailItemDto>();
        }

        worklistTimelineEventsTypeDto.EventDetailItems.Add(eventDetailItemDto);
        worklistTimelineEventsTypeDto.EventDetailItems = worklistTimelineEventsTypeDto.EventDetailItems.OrderBy(x => x.EventDateTime).ToList();
    }

    private string ToMonthName(DateTime dateTime)
    {
        return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(dateTime.Month);
    }

    //private string ToShortMonthName(this DateTime dateTime)
    //{
    //    return CultureInfo.CurrentCulture.DateTimeFormat.GetAbbreviatedMonthName(dateTime.Month);
    //}


}