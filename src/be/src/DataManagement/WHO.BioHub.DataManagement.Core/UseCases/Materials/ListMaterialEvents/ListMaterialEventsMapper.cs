using System.Globalization;
using WHO.BioHub.Models.Models;
using WHO.BioHub.Shared.Dto;
using WHO.BioHub.Shared.Enums;
using WHO.BioHub.Shared.Worklists;

namespace WHO.BioHub.DataManagement.Core.UseCases.MaterialEvents.ListMaterialEvents;

public interface IListMaterialEventsMapper
{
    WorklistTimeline Map(Material material);
}

public class ListMaterialEventsMapper : IListMaterialEventsMapper
{
    public WorklistTimeline Map(Material material)
    {        

        WorklistTimeline worklistTimeline = new WorklistTimeline();

        worklistTimeline.Events = GetList(material);

        worklistTimeline.TimelineTitle = string.Empty;        

        return worklistTimeline;
    }

    private List<WorklistTimelineEventsDayDto> GetList(Material material)
    {
        List<WorklistTimelineEventsDayDto> list = new List<WorklistTimelineEventsDayDto>();

        string label = string.Empty;

        DateTime creationDate = material.StartingDate.GetValueOrDefault() == default ? DateTime.UtcNow : material.StartingDate.GetValueOrDefault();

        AddOrUpdateTimelineEventsDays(list, WorklistEventNames.MaterialCreation, 0, creationDate, WorklistEventType.Milestone);

        DateTime? culturingResultDate = material.CulturingResultDate;        

        if (culturingResultDate != null)
        {
            label = GetResultTypeLabel(material.CulturingResult, WorklistEventNames.CulturingResult);           
        
            AddOrUpdateTimelineEventsDays(list, label, (culturingResultDate.GetValueOrDefault().Date - creationDate.Date).Days, culturingResultDate.GetValueOrDefault(), WorklistEventType.Milestone);
        }

        DateTime? qualityControlResultDate = material.QualityControlResultDate;

        if (qualityControlResultDate != null)
        {
            label = GetResultTypeLabel(material.QualityControlResult, WorklistEventNames.QualityControlResult);

            AddOrUpdateTimelineEventsDays(list, label, (qualityControlResultDate.GetValueOrDefault().Date - creationDate.Date).Days, qualityControlResultDate.GetValueOrDefault(), WorklistEventType.Milestone);
        }


        DateTime? GSDAnalysisResultDate = material.GSDAnalysisResultDate;

        if (GSDAnalysisResultDate != null)
        {
            label = GetResultTypeLabel(material.GSDAnalysisResult, WorklistEventNames.GSDAnalysisResult);
            AddOrUpdateTimelineEventsDays(list, label, (GSDAnalysisResultDate.GetValueOrDefault().Date - creationDate.Date).Days, GSDAnalysisResultDate.GetValueOrDefault(), WorklistEventType.Milestone);
        }


        DateTime? GSDUploadingDate = material.GSDUploadingDate;

        if (GSDUploadingDate != null)
        {
            AddOrUpdateTimelineEventsDays(list, $"{WorklistEventNames.GSDUploadingStatus} - Uploaded", (GSDUploadingDate.GetValueOrDefault().Date - creationDate.Date).Days, GSDUploadingDate.GetValueOrDefault(), WorklistEventType.Milestone);
        }


        var aliquotsAddedHistory = material.MaterialsHistory.Where(x => x.LastAliquotsAdditionDate != null).ToList();
        List<DateTime?> lastAliquotsAdditionDates = aliquotsAddedHistory.Select(x => x.LastAliquotsAdditionDate).ToList();

        if (material.LastAliquotsAdditionDate != null)
        {
            lastAliquotsAdditionDates.Add(material.LastAliquotsAdditionDate);           
        }

        lastAliquotsAdditionDates = lastAliquotsAdditionDates.Distinct().ToList();

        foreach (var lastAliquotsAdditionDate in lastAliquotsAdditionDates)
        {
            var totalVials = aliquotsAddedHistory.Where(x => x.LastAliquotsAdditionDate == lastAliquotsAdditionDate).Sum(x => x.AddedAliquots);

            if (material.LastAliquotsAdditionDate == lastAliquotsAdditionDate)
            {
                totalVials += material.AddedAliquots;
            }

            AddOrUpdateTimelineEventsDays(list, $"{WorklistEventNames.NumberOfVialsProduced} - {totalVials}", (lastAliquotsAdditionDate.GetValueOrDefault().Date - creationDate.Date).Days, lastAliquotsAdditionDate.GetValueOrDefault(), WorklistEventType.Milestone);
        }
                       

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
                        AddOrUpdateTimelineEventsDays(list, string.Empty, (date.Date - creationDate.Date).Days, date, WorklistEventType.None);
                    }
                }
            }
        }
        list = list.OrderBy(x => x.Date).ToList();
        SetPositions(list);
        return list;
    }

    private string GetResultTypeLabel(ResultType? resultType, string eventName)
    {
        string label = eventName;
        if (resultType != null)
        {
            label = $"{label} - {resultType.ToString()}";
        }

        return label;
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




    private void AddOrUpdateTimelineEventsDays(
        List<WorklistTimelineEventsDayDto> worklistTimelineEventDays,
        string label,
        int dateZeroBased,
        DateTime dateTime,
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
            //worklistTimelineDayEvents.Stage = stage;

            if (eventType != WorklistEventType.None)
            {
                AddWorklistTimelineEventsType(label, dateTime, eventType, worklistTimelineDayEvents);
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

                AddWorklistTimelineEventsType(label, dateTime, eventType, existingElement);

            }
        }
    }

    private void AddWorklistTimelineEventsType(string label, DateTime dateTime, WorklistEventType eventType, WorklistTimelineEventsDayDto worklistTimelineDayEventsDto)
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
}