using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smartstore.CustomDashboard.Models
{
    public class PublicInfoModel
    {
        public List<BreakPoints> BreakPoints { get; set; } = [];
    }

    public class BreakPoints
    {
        public List<WidgetLine> WidgetLines { get; set; } = [];
    }

    public class WidgetLine
    {
        public List<WidgetItem> Widgets { get; set; } = [];
    }

    public class WidgetItem
    {
        public string SystemName { get; set; }
        // TODO: Really?
        public string ViewComponentName { get; set; }

        // TODO: Either position or display order > Decide!
        public string Position { get; set; }
        public int DisplayOrder { get; set; }


        // TODO: Really? 
        public object ViewComponentArguments { get; set; }
    }
}
