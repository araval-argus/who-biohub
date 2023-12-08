using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Utils;
using System.Globalization;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.DataManagement.Core.UseCases.SMTA2WorkflowItems.ListSMTA2WorkflowEventItem;

public interface IListSMTA2WorkflowItemEventMapper
{
    List<WorklistTimeline> Map(SMTA2WorkflowItem entity);
}

public class ListSMTA2WorkflowItemEventMapper : IListSMTA2WorkflowItemEventMapper
{

    public List<WorklistTimeline> Map(SMTA2WorkflowItem entity)
    {

        List<WorklistTimeline> overallList = new List<WorklistTimeline>();

        WorklistTimeline worklistTimeline = new WorklistTimeline();

        worklistTimeline.Events = GetList(entity);

        worklistTimeline.TimelineTitle = string.Empty;

        overallList.Add(worklistTimeline);

        return overallList;
    }

    private List<WorklistTimelineEventsDayDto> GetList(SMTA2WorkflowItem entity)
    {
        List<WorklistTimelineEventsDayDto> list = new List<WorklistTimelineEventsDayDto>();       

        DateTime smtaRequestDate = entity.SMTA2WorkflowHistoryItems.Any() ? (entity.SMTA2WorkflowHistoryItems.OrderBy(x => x.OperationDate).FirstOrDefault(x => x.Status == SMTA2WorkflowStatus.SubmitSMTA2)?.OperationDate).GetValueOrDefault() : entity.OperationDate.GetValueOrDefault();

        AddOrUpdateTimelineEventsDays(list, WorklistEventNames.SMTA2Request, 0, smtaRequestDate, WorklistEventStageNames.SMTA2, WorklistEventType.Milestone);

        DateTime? approvedDate = GetApprovedEventDate(entity, SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval);

        if (approvedDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ApprovedSMTA2, (approvedDate.GetValueOrDefault().Date - smtaRequestDate.Date).Days, approvedDate.GetValueOrDefault(), WorklistEventStageNames.SMTA2, WorklistEventType.Milestone);
        }


        var submitSMTADates = GetMinorEventDates(entity, SMTA2WorkflowStatus.SubmitSMTA2, true);

        if (submitSMTADates != null && submitSMTADates.Any())
        {
            var firstSubmit = submitSMTADates.OrderBy(x => x.Date).FirstOrDefault();
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.SubmitSMTA2, (firstSubmit.Date - smtaRequestDate.Date).Days, firstSubmit, WorklistEventStageNames.SMTA2, WorklistEventType.Minor);

            foreach (var submitSMTADate in submitSMTADates)
            {
                if (firstSubmit != submitSMTADate)
                {
                    AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReSubmitSMTA2, (submitSMTADate.Date - smtaRequestDate.Date).Days, submitSMTADate, WorklistEventStageNames.SMTA2, WorklistEventType.Minor);
                }
            }
        }

        var rejectSMTADates = GetMinorEventDates(entity, SMTA2WorkflowStatus.WaitingForSMTA2SECsApproval, false);

        if (rejectSMTADates != null && rejectSMTADates.Any())
        {
            foreach (var rejectSMTADate in rejectSMTADates)
            {
                AddOrUpdateTimelineEventsDays(list, WorklistEventNames.ReturnedSMTA2, (rejectSMTADate.Date - smtaRequestDate.Date).Days, rejectSMTADate, WorklistEventStageNames.SMTA2, WorklistEventType.Minor);
            }
        }



        DateTime? smtaCompletedDate = entity.Status == SMTA2WorkflowStatus.SMTA2WorkflowComplete ? entity.OperationDate.GetValueOrDefault() : null;

        if (smtaCompletedDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, WorklistEventNames.SMTA2Completed, (smtaCompletedDate.GetValueOrDefault().Date - smtaRequestDate.Date).Days, smtaCompletedDate.GetValueOrDefault(), WorklistEventStageNames.SMTA2, WorklistEventType.Milestone);
        };

        list = list.OrderBy(x => x.Date).ToList();

        if (list.Any())
        {

            DateTime? lowerDate = list.FirstOrDefault()?.Date;

            DateTime? higherDate = list.LastOrDefault()?.Date;

            DateTime firstDate = lowerDate.Value.AddDays(-7);

            DateTime lastDate = higherDate.Value.AddDays(3);
          
            if (firstDate != lastDate)
            {
                for (var date = firstDate; date < lastDate; date = date.AddDays(1))
                {
                    if (!list.Any(x => x.Date.Date == date.Date))
                    {
                        AddOrUpdateTimelineEventsDays(list, string.Empty, (date.Date - smtaRequestDate.Date).Days, date, string.Empty, WorklistEventType.None);
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


    private DateTime? GetApprovedEventDate(SMTA2WorkflowItem entity, SMTA2WorkflowStatus previousStatus)
    {
        if (entity.PreviousStatus == previousStatus && entity.LastSubmissionApproved == true)
        {
            return entity.OperationDate != null ? entity.OperationDate.GetValueOrDefault() : null;
        }
        else
        {
            if (entity.SMTA2WorkflowHistoryItems == null)
            {
                return null;
            }

            var historyEntity = entity.SMTA2WorkflowHistoryItems.Where(x => x.PreviousStatus == previousStatus && x.LastSubmissionApproved == true).FirstOrDefault();

            if (historyEntity != null)
            {
                return historyEntity.OperationDate != null ? historyEntity.OperationDate.GetValueOrDefault() : null;
            }
        }

        return null;
    }


    private List<DateTime> GetMinorEventDates(SMTA2WorkflowItem entity, SMTA2WorkflowStatus previousStatus, bool lastSubmissionApproved)
    {
        var list = new List<DateTime>();
        if (entity.PreviousStatus == previousStatus && entity.LastSubmissionApproved == lastSubmissionApproved)
        {
            if (entity.OperationDate != null)
            {
                list.Add(entity.OperationDate.GetValueOrDefault());
            }
        }

        if (entity.SMTA2WorkflowHistoryItems != null && entity.SMTA2WorkflowHistoryItems.Any())
        {
            var historyEntityList = entity.SMTA2WorkflowHistoryItems.Where(x => x.PreviousStatus == previousStatus && x.LastSubmissionApproved == lastSubmissionApproved).ToList();

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