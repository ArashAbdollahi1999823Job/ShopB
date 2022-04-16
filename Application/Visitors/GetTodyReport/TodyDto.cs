namespace Application.Visitors.GetTodyReport;

public class TodyDto
{
    public long PageView { get; set; }
    public long Visitors { get; set; }
    public float ViewPerVisitor { get; set; }
    public VisitCountDto VisitPerHour { get; set; }
}