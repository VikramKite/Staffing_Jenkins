using System;

namespace staffing.data.models.Staffing
{
    public class StaffingListModel
    {
        public int staffing_id { get; set; }
        public string job_title_name { get; set; }
        public bool epic_position { get; set; }
        public int total_no_of_positions { get; set; }
        public int no_of_positions_filled { get; set; }
        public string job_location_name { get; set; }
        public DateTime? date_job_received { get; set; }
        public string client_name { get; set; }
        public string client_manager_name { get; set; }
        public string job_description { get; set; }

        public string assigned_to_whom_name { get; set; }
        public string account_manager_name { get; set; }
        public string follow_up_action { get; set; }
        public string position_type_name { get; set; }
        public string rate_range_w2 { get; set; }
        public string rate_range_c2c_1099 { get; set; }
        public string duration { get; set; }
        public string t_e_paid { get; set; }
        public string position_status_name { get; set; }

        public DateTime? doc_date { get; set; }
        public string created_datetime => doc_date == null || doc_date.HasValue == false ? string.Empty :
            (doc_date.Value.Date.Equals(DateTime.Now.Date) ? $"Today, {doc_date.Value.ToString("MMMM dd, y")}" : $"{doc_date.Value.ToString("MMMM dd, y")}");
    }
}
