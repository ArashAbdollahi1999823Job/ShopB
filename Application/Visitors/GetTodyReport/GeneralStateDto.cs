namespace Application.Visitors.GetTodyReport;

public class GeneralStateDto
{
    public long TotalPageViews { get; set; }
    public long TotalVisitors { get; set; }
    public long PageViewPerVisit { get; set; }
    public VisitCountDto VisitPerDay { get; set; }
}