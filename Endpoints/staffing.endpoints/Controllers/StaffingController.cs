using staffing.data.ef;
using staffing.data.models;
using staffing.data.models.Common;
using staffing.data.models.Common.Post;
using staffing.data.models.Lookups;
using staffing.endpoints.Controllers.Common;
using staffing.endpoints.Filters;
using staffing.endpoints.Models;
using staffing.interfaces.processor.Lookups;
using staffing.interfaces.processor.Staffing;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace staffing.endpoints.Controllers
{
    [AuthorizeUserFilter(Roles = "1,2")]
    [ActionFilter]
    public class StaffingController : BaseController
    {

        InternalSystemEntities _entities = new InternalSystemEntities();

        private readonly IStaffingProcessor _staffingProcessor;
        private readonly ILookupsProcessor _lookupsProcesor;
        public StaffingController(IStaffingProcessor processor, ILookupsProcessor lookupsProcesor)
        {
            _staffingProcessor = processor;
            _lookupsProcesor = lookupsProcesor;
        }

        // GET: Staffing
        [HttpGet]
        public ActionResult Dashboard()
        {
            //Session["UserName"] = "Vikram";
            //Response.Cookies["MemberRole"].Value = "Admin";
            int a = (from u in _entities.staffings
                     select (int?)u.total_no_of_positions).Sum() ?? 0;
            int b = (from u in _entities.staffings
                     select (int?)u.no_of_positions_filled).Sum() ?? 0;

            DashboardViewModel model = new DashboardViewModel();
            model.position_statuswisecount = _entities.SP_GetPositionStatus().ToList();
            model.total_no_of_jobs = (int)_entities.staffings.ToList().Count();
            model.no_of_clients = (int)_entities.clients.ToList().Count();
            //model.total_no_of_positions = Convert.ToInt32(a);
            model.total_no_of_positions = (int)_entities.staffings.ToList().Count(x => x.is_active == true);
            model.no_of_positions_filled = Convert.ToInt32(b);

            using (var cmd = _entities.Database.Connection.CreateCommand())
            {
                _entities.Database.Connection.Open();
                cmd.CommandText = "EXEC SP_GetClientWiseReport";
                using (var reader = cmd.ExecuteReader())
                {
                    model.clientwisestatus_list = Read(reader).ToList();
                }
            }


            return View(model);
        }

        [NonAction]
        public List<Dictionary<string, object>> Read(DbDataReader reader)
        {
            List<Dictionary<string, object>> expandolist = new List<Dictionary<string, object>>();
            foreach (var item in reader)
            {
                IDictionary<string, object> expando = new ExpandoObject();
                foreach (PropertyDescriptor propertyDescriptor in TypeDescriptor.GetProperties(item))
                {
                    var obj = propertyDescriptor.GetValue(item);
                    expando.Add(propertyDescriptor.Name, obj);
                }
                expandolist.Add(new Dictionary<string, object>(expando));
            }
            return expandolist;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var resp = await _staffingProcessor.GetAll();
            if (resp.success == false)
                TempData["error_message"] = resp.message;

            return View(resp.data);
        }

        [HttpGet]
        public async Task<ActionResult> AddNewStaffing()
        {
            StaffingAddUpdateViewModel model = new StaffingAddUpdateViewModel
            {
                position_status_id = 1,
                total_no_of_positions = 1,
                epic_position = "N",
                position_type_id = 1,
                t_e_paid = "No",
                no_of_positions_filled = 0,
                positiontype_list = await _lookupsProcesor.GetPositionTypes(),
                positionstatus_list = await _lookupsProcesor.GetPositionStatus()
            };
            return View(model);
        }

        [Route("{id}")]
        public async Task<ActionResult> UpdateStaffing(int id)
        {
            StaffingAddUpdateViewModel vm = new StaffingAddUpdateViewModel
            {
                positiontype_list = await _lookupsProcesor.GetPositionTypes(),
                positionstatus_list = await _lookupsProcesor.GetPositionStatus()
            };

            ResponseDataModel<StaffingModel> userresp = await _staffingProcessor.GetById(id);
            StaffingModel user = userresp.data;
            if (userresp.success == false || userresp.data == null)
            {
                vm.error_message = userresp.message;
                return View("AddNewStaffing", vm);
            }

            vm.staffing_id = user.staffing_id;
            vm.job_title_id = user.job_title_id;
            vm.job_title_name = user.job_title_name;
            vm.epic_position = user.epic_position == true ? "Y" : "N";
            vm.total_no_of_positions = user.total_no_of_positions;
            vm.no_of_positions_filled = user.no_of_positions_filled;
            vm.job_location_id = user.job_location_id;
            vm.job_location_name = user.job_location_name;
            vm.date_job_received = user.date_job_received != null ? user.date_job_received : null;
            vm.client_id = user.client_id;
            vm.client_name = user.client_name;
            vm.client_manager_id = user.client_manager_id;
            vm.client_manager_name = user.client_manager_name;
            vm.job_description = user.job_description;
            vm.assigned_to_whom_id = user.assigned_to_whom_id;
            vm.assigned_to_whom_name = user.assigned_to_whom_name;
            vm.account_manager_id = user.account_manager_id;
            vm.account_manager_name = user.account_manager_name;
            vm.position_status_id = user.position_status_id;
            vm.follow_up_action = user.follow_up_action;
            vm.position_type_id = user.position_type_id;
            vm.rate_range_w2 = user.rate_range_w2;
            vm.rate_range_c2c_1099 = user.rate_range_c2c_1099;
            vm.duration = user.duration;
            vm.t_e_paid = user.t_e_paid == true ? "Yes" : "No";
            vm.job_closed_date = user.job_closed_date != null ? user.job_closed_date : null;
            return View("AddNewStaffing", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddNewStaffing(StaffingAddUpdateViewModel pd)
        {
            if (string.IsNullOrEmpty(pd.job_title_name))
                ModelState.AddModelError("job_title_name", "Job Title is required");
            if (!string.IsNullOrEmpty(pd.job_description) && pd.job_description.Length > 1000)
                ModelState.AddModelError("job_description", "Job Description cannot be longer than 1000 characters. you have entered " + pd.job_description.Length + " characters");

            if (string.IsNullOrEmpty(pd.epic_position))
                ModelState.AddModelError("epic_position", "Epic Position is required");
            if (pd.position_type_id == 0)
                ModelState.AddModelError("position_type_id", "Position Type is required");
            if (string.IsNullOrEmpty(pd.t_e_paid))
                ModelState.AddModelError("t_e_paid", "T&E is required");

            if (pd.position_status_id != 0 && (pd.position_status_id == 4 || pd.position_status_id == 5) && (pd.job_closed_date == null || pd.job_closed_date == DateTime.MinValue))
            {
                ModelState.AddModelError("job_closed_date", "Job Closed Date is required");
            }

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState
                                 where item.Value.Errors.Any()
                                 select item.Value.Errors[0].ErrorMessage).ToList();

                pd.positionstatus_list = await _lookupsProcesor.GetPositionStatus();
                pd.positiontype_list = await _lookupsProcesor.GetPositionTypes();
                //pd.error_message = errorList[0].Length != 0 ? Convert.ToString(errorList[0]) : string.Empty;
                return View("AddNewStaffing", pd);
            }

            if (pd.staffing_id == 0)
            {
                await CreateStaffing(pd);
            }
            else
            {
                int id = await UpdateStaffing(pd);
            }
            return RedirectToAction("Index", "Staffing");
        }

        [NonAction]
        private async Task<int> CreateStaffing(StaffingAddUpdateViewModel data)
        {
            StaffingModel prod = AddOrUpdate(data);

            var resp = await _staffingProcessor.Insert(prod, CurrentUserId, CurrentDateTime);
            return resp.data;
        }

        [NonAction]
        private async Task<int> UpdateStaffing(StaffingAddUpdateViewModel data)
        {
            StaffingModel prod = AddOrUpdate(data);
            var resp = await _staffingProcessor.Update(data.staffing_id, prod, CurrentUserId, CurrentDateTime);
            return resp.data;
        }

        [NonAction]
        private StaffingModel AddOrUpdate(StaffingAddUpdateViewModel data)
        {
            StaffingModel prod = new StaffingModel
            {
                job_title_id = data.job_title_id,
                job_title_name = !string.IsNullOrEmpty(data.job_title_name) ? data.job_title_name.Trim() : string.Empty,
                epic_position = (!string.IsNullOrEmpty(data.epic_position) && data.epic_position == "Y") ? true : false,
                total_no_of_positions = (int)data.total_no_of_positions != 0 ? (int)data.total_no_of_positions : 1,
                no_of_positions_filled = (int)data.no_of_positions_filled != 0 ? (int)data.no_of_positions_filled : 0,
                job_location_id = data.job_location_id,
                job_location_name = !string.IsNullOrEmpty(data.job_location_name) ? data.job_location_name.Trim() : string.Empty,
                date_job_received = data.date_job_received != null || data.date_job_received != DateTime.MinValue ? data.date_job_received : null,
                client_id = data.client_id,
                client_name = !string.IsNullOrEmpty(data.client_name) ? data.client_name.Trim() : string.Empty,
                client_manager_id = data.client_manager_id,
                client_manager_name = !string.IsNullOrEmpty(data.client_manager_name) ? data.client_manager_name.Trim() : string.Empty,
                job_description = !string.IsNullOrEmpty(data.job_description) ? data.job_description.Trim() : string.Empty,
                assigned_to_whom_id = data.assigned_to_whom_id,
                assigned_to_whom_name = !string.IsNullOrEmpty(data.assigned_to_whom_name) ? data.assigned_to_whom_name.Trim() : string.Empty,
                account_manager_id = data.account_manager_id,
                account_manager_name = !string.IsNullOrEmpty(data.account_manager_name) ? data.account_manager_name.Trim() : string.Empty,
                position_status_id = data.position_status_id,
                follow_up_action = !string.IsNullOrEmpty(data.follow_up_action) ? data.follow_up_action.Trim() : string.Empty,
                position_type_id = data.position_type_id,
                rate_range_w2 = !string.IsNullOrEmpty(data.rate_range_w2) ? data.rate_range_w2.Trim() : string.Empty,
                rate_range_c2c_1099 = !string.IsNullOrEmpty(data.rate_range_c2c_1099) ? data.rate_range_c2c_1099.Trim() : string.Empty,
                duration = !string.IsNullOrEmpty(data.duration) ? data.duration.Trim() : string.Empty,
                t_e_paid = (!string.IsNullOrEmpty(data.t_e_paid) && data.t_e_paid == "Yes") ? true : false,
                job_closed_date = data.job_closed_date != null || data.job_closed_date != DateTime.MinValue ? data.job_closed_date : null
            };
            return prod;
        }

        [HttpPost]
        public async Task<JsonResult> GetDropdownList(string search_text, string search_in_master)
        {
            if (string.IsNullOrEmpty(search_in_master))
                return Json(null, JsonRequestBehavior.AllowGet);

            List<DropdownListModel> resp = new List<DropdownListModel>();
            resp = await _lookupsProcesor.GetDropdownList(search_text, search_in_master);

            return Json(resp, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> Delete(string staffing_id)
        {
            try
            {
                if (string.IsNullOrEmpty(Convert.ToString(staffing_id)))
                    return Json("Error", JsonRequestBehavior.AllowGet);

                PatchPostModel data = new PatchPostModel();
                data.changed.Add("is_active", false.ToString());

                var resp = await _staffingProcessor.Modify(Convert.ToInt32(staffing_id), data, CurrentUserId, CurrentDateTime);

                if (resp.success)
                    return Json(data: true, behavior: JsonRequestBehavior.AllowGet);
                else
                    return Json(data: false, behavior: JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}