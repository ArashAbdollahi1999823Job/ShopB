using System.Collections.Generic;

namespace Application.Visitors.GetTodyReport;

public class ResultTodayReportDto
{
    public GeneralStateDto GeneralStats { get; set; }
    public TodyDto Tody { get; set; }

    public List<VisitorsDto> Visitor { get; set; }

}