using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WHO.BioHub.Shared.Utils
{
    public static class EnumWorklistStatusExtensions
    {
        public static string StatusName(this Enum value)
        {
            var attributes = (StatusDescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StatusDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            return string.Empty;
        }

        public static string WorklistItemApprovedInfo(this Enum value)
        {
            var attributes = (StatusDescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StatusDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].WorklistItemApprovedInfo;
            }
            return string.Empty;
        }

        public static string WorklistItemRejectedInfo(this Enum value)
        {
            var attributes = (StatusDescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StatusDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].WorklistItemRejectedInfo;
            }
            return string.Empty;
        }

        public static string WorklistItemRejectedTitle(this Enum value)
        {
            var attributes = (StatusDescriptionAttribute[])value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(StatusDescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].WorklistItemRejectedTitle;
            }
            return string.Empty;
        }
    }

    public class StatusDescriptionAttribute : DescriptionAttribute
    {        
        public string WorklistItemApprovedInfo { get; private set; }
        public string WorklistItemRejectedInfo { get; private set; }
        public string? WorklistItemRejectedTitle { get; private set; }

        public StatusDescriptionAttribute(string statusName, string worklistItemApprovedInfo, string worklistItemRejectedInfo, string? worklistItemRejectedTitle = null) : base(statusName)
        {
            WorklistItemApprovedInfo = worklistItemApprovedInfo;
            WorklistItemRejectedInfo = worklistItemRejectedInfo;
            WorklistItemRejectedTitle = worklistItemRejectedTitle ?? string.Empty;
        }
    }
}
