using System;

namespace staffing.data.models
{
    public class StaffingModel
    {
        public int staffing_id { get; set; }
        public int job_title_id { get; set; }
        public string job_title_name { get; set; }

        public bool epic_position { get; set; }
        public int total_no_of_positions { get; set; }
        public int no_of_positions_filled { get; set; }
        public int job_location_id { get; set; }
        public string job_location_name { get; set; }

        public DateTime? date_job_received { get; set; }
        public int client_id { get; set; }
        public string client_name { get; set; }

        public int client_manager_id { get; set; }
        public string client_manager_name { get; set; }

        public string job_description { get; set; }
        public int assigned_to_whom_id { get; set; }
        public string assigned_to_whom_name { get; set; }

        public int account_manager_id { get; set; }
        public string account_manager_name { get; set; }

        public int position_status_id { get; set; }
        public string follow_up_action { get; set; }
        public int position_type_id { get; set; }
        public string rate_range_w2 { get; set; }
        public string rate_range_c2c_1099 { get; set; }
        public string duration { get; set; }
        public bool t_e_paid { get; set; }
        public DateTime? job_closed_date { get; set; }
    }
}
