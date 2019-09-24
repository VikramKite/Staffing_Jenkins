using staffing.data.models;
using staffing.data.models.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace staffing.endpoints.Models
{
    public class StaffingAddUpdateViewModel
    {
        public string error_message { get; set; }
        public int staffing_id { get; set; }
        public int job_title_id { get; set; }
        [Required(ErrorMessage = "Job Title is required")]
        public string job_title_name { get; set; }

        [Required(ErrorMessage = "Epic Position is required")]
        public string epic_position { get; set; }

        public int? total_no_of_positions { get; set; }
        public int? no_of_positions_filled { get; set; }
        public int job_location_id { get; set; }
        [Required(ErrorMessage = "Job Location is required")]
        public string job_location_name { get; set; }

        [Required(ErrorMessage = "Job Received Date is required")]
        public DateTime? date_job_received { get; set; }

        public int client_id { get; set; }
        [Required(ErrorMessage = "Client Name is required")]
        public string client_name { get; set; }

        public int client_manager_id { get; set; }
        public string client_manager_name { get; set; }

        [Required(ErrorMessage = "Job Description is required")]
        public string job_description { get; set; }
        public int assigned_to_whom_id { get; set; }
        public string assigned_to_whom_name { get; set; }

        public int account_manager_id { get; set; }
        [Required(ErrorMessage = "Account Manager Name is required")]
        public string account_manager_name { get; set; }

        [Required(ErrorMessage = "Job Status is required")]
        public int position_status_id { get; set; }
        public List<PositionStatusListModel> positionstatus_list { get; set; }

        public string follow_up_action { get; set; }
        [Required(ErrorMessage = "Position Type is required")]
        public int position_type_id { get; set; }
        public List<PositionTypeListModel> positiontype_list { get; set; }

        public string rate_range_w2 { get; set; }
        public string rate_range_c2c_1099 { get; set; }
        public string duration { get; set; }

        [Required(ErrorMessage = "T & E Paid is required")]
        public string t_e_paid { get; set; }

        public DateTime? job_closed_date { get; set; }
    }
}