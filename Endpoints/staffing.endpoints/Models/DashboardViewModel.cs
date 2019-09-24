using staffing.data.ef;
using System.Collections.Generic;

namespace staffing.endpoints.Models
{
    public class DashboardViewModel
    {
        public int total_no_of_positions { get; set; }
        public int no_of_positions_filled { get; set; }
        public int total_no_of_jobs { get; set; }
        public int no_of_clients { get; set; }

        public List<SP_GetPositionStatus_Result> position_statuswisecount;
        //public List<SP_GetAllClientStatusReport_Result> client_wise_report;
        public List<Dictionary<string, object>> clientwisestatus_list { get; set; }
    }
}