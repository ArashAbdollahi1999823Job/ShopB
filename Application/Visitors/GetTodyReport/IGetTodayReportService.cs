using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Visitors.GetTodyReport
{
    public interface IGetTodayReportService
    {
        ResultTodayReportDto Execute();
    }
}
