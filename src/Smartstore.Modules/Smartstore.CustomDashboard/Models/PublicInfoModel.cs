using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Smartstore.CustomDashboard.Models
{
    public class PublicInfoModel
    {
        public List<BreakPoint> BreakPoints { get; set; } = new();
    }

    public class BreakPoint
    {
        public List<WidgetLine> WidgetLines { get; set; } = new();
    }

    public class WidgetLine
    {
        [JsonPropertyName("WidgetItem")]
        public List<WidgetItem> Widgets { get; set; } = new();
    }

    public class WidgetItem
    {
        public string SystemName { get; set; }
        public string ViewComponentName { get; set; }
        public string Namespace { get; set; }
        public string Position { get; set; }
        public int DisplayOrder { get; set; }
        public object ViewComponentArguments { get; set; }
    }
}

