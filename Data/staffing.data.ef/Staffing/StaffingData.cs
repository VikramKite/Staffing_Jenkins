using staffing.data.ef.Common;
using staffing.data.models;
using staffing.data.models.Common.Post;
using staffing.data.models.Staffing;
using staffing.interfaces.data.Staffing;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace staffing.data.ef.Staffing
{
    public class StaffingData : BaseData, IStaffingData
    {
        public async Task<List<StaffingListModel>> GetAll()
        {
            List<StaffingListModel> resp;
            using (var db = new InternalSystemEntities())
            {
                resp = await (from a in db.staffings
                              join p in db.job_title on a.job_title_id equals p.job_title_id
                              join s in db.position_status on a.position_status_id equals s.position_status_id
                              join q in db.job_location on a.job_location_id equals q.job_location_id into jlocationData
                              from aq in jlocationData.DefaultIfEmpty()
                              join r in db.clients on a.client_id equals r.client_id into clientData
                              from ar in clientData.DefaultIfEmpty()
                              join t in db.assigned_to_whom on a.assigned_to_whom_id equals t.assigned_to_whom_id into assignedData
                              from at in assignedData.DefaultIfEmpty()
                              join u in db.account_manager on a.account_manager_id equals u.account_manager_id into accmanagerData
                              from au in accmanagerData.DefaultIfEmpty()
                              join v in db.position_type on a.position_type_id equals v.position_type_id into positypeData
                              from av in positypeData.DefaultIfEmpty()
                              join w in db.client_manager on a.client_manager_id equals w.client_manager_id into clientmanagerData
                              from aw in clientmanagerData.DefaultIfEmpty()
                              where a.is_active == true
                              select new StaffingListModel
                              {
                                  staffing_id = a.staffing_id,
                                  job_title_name = p.job_title_name,
                                  epic_position = a.epic_position == true ? true : false,
                                  total_no_of_positions = a.total_no_of_positions,
                                  no_of_positions_filled = a.no_of_positions_filled,
                                  job_location_name = aq == null ? string.Empty : aq.job_location_name,
                                  date_job_received = a.date_job_received,
                                  client_name = ar == null ? string.Empty : ar.client_name,
                                  client_manager_name = aw.client_manager_name,
                                  job_description = a.job_description,
                                  assigned_to_whom_name = at == null ? string.Empty : at.assigned_to_whom_name,
                                  account_manager_name = au == null ? string.Empty : au.account_manager_name,
                                  follow_up_action = a.follow_up_action,
                                  position_type_name = av == null ? string.Empty : av.position_type_name,
                                  rate_range_w2 = a.rate_range_w2,
                                  rate_range_c2c_1099 = a.rate_range_c2c_1099,
                                  duration = a.duration,
                                  t_e_paid = a.t_e_paid == true ? "Yes" : "No",
                                  position_status_name = s.position_status_name,
                                  doc_date = a.created_datetime
                              }).ToListAsync();
            }

            return resp.Count > 0 ? resp : null;
        }

        public async Task<StaffingModel> GetById(int id)
        {
            using (var db = new InternalSystemEntities())
            {
                var data = await db.staffings.FindAsync(id);
                if (data == null || data.staffing_id == 0)
                {
                    return null;
                }
                var model = new StaffingModel()
                {
                    staffing_id = data.staffing_id,
                    job_title_id = data.job_title_id,
                    epic_position = data.epic_position,
                    total_no_of_positions = data.total_no_of_positions,
                    no_of_positions_filled = data.no_of_positions_filled,
                    job_location_id = data.job_location_id.HasValue ? data.job_location_id.Value : 0,
                    date_job_received = data.date_job_received != null || data.date_job_received != DateTime.MinValue ? data.date_job_received : null,
                    client_id = data.client_id.HasValue ? data.client_id.Value : 0,
                    client_manager_id = data.client_manager_id.HasValue ? data.client_manager_id.Value : 0,
                    job_description = data.job_description,
                    assigned_to_whom_id = data.assigned_to_whom_id.HasValue ? data.assigned_to_whom_id.Value : 0,
                    account_manager_id = data.account_manager_id.HasValue ? data.account_manager_id.Value : 0,
                    position_status_id = data.position_status_id.HasValue ? data.position_status_id.Value : 0,
                    follow_up_action = data.follow_up_action,
                    position_type_id = data.position_type_id.HasValue ? data.position_type_id.Value : 0,
                    rate_range_w2 = data.rate_range_w2,
                    rate_range_c2c_1099 = data.rate_range_c2c_1099,
                    duration = data.duration,
                    t_e_paid = data.t_e_paid,
                    job_closed_date = data.job_closed_date != null || data.job_closed_date != DateTime.MinValue ? data.job_closed_date : null
                };

                var job_title = await db.job_title.FindAsync(data.job_title_id);
                if (job_title == null || job_title.job_title_id == 0)
                    model.job_title_name = string.Empty;
                else
                    model.job_title_name = job_title.job_title_name;

                var job_location = await db.job_location.FindAsync(data.job_location_id);
                if (job_location == null || job_location.job_location_id == 0)
                    model.job_location_name = string.Empty;
                else
                    model.job_location_name = job_location.job_location_name;

                var clients = await db.clients.FindAsync(data.client_id);
                if (clients == null || clients.client_id == 0)
                    model.client_name = string.Empty;
                else
                    model.client_name = clients.client_name;

                var assigned_to_whom = await db.assigned_to_whom.FindAsync(data.assigned_to_whom_id);
                if (assigned_to_whom == null || assigned_to_whom.assigned_to_whom_id == 0)
                    model.assigned_to_whom_name = string.Empty;
                else
                    model.assigned_to_whom_name = assigned_to_whom.assigned_to_whom_name;

                var account_manager = await db.account_manager.FindAsync(data.account_manager_id);
                if (account_manager == null || account_manager.account_manager_id == 0)
                    model.account_manager_name = string.Empty;
                else
                    model.account_manager_name = account_manager.account_manager_name;

                var client_manager = await db.client_manager.FindAsync(data.client_manager_id);
                if (client_manager == null || client_manager.client_manager_id == 0)
                    model.client_manager_name = string.Empty;
                else
                    model.client_manager_name = client_manager.client_manager_name;

                return model;
            }
        }

        public async Task<int> Insert(StaffingModel data, int adminId, DateTime currentDt)
        {
            staffing _staffing = new staffing
            {
                epic_position = data.epic_position,
                total_no_of_positions = data.total_no_of_positions,
                no_of_positions_filled = data.no_of_positions_filled,
                date_job_received = data.date_job_received != null || data.job_closed_date != DateTime.MinValue ? data.date_job_received : null,
                job_description = data.job_description,
                position_status_id = data.position_status_id,
                follow_up_action = data.follow_up_action,
                position_type_id = data.position_type_id,
                rate_range_w2 = data.rate_range_w2,
                rate_range_c2c_1099 = data.rate_range_c2c_1099,
                duration = data.duration,
                t_e_paid = data.t_e_paid,
                job_closed_date = data.job_closed_date != null || data.job_closed_date != DateTime.MinValue ? data.date_job_received : null,
                is_active = true,
                created_by_user_id = adminId,
                created_datetime = currentDt,
                last_updated_by_user_id = adminId,
                last_updated_datetime = currentDt
            };

            int id = await GetJobTitleId(data.job_title_name);
            if (id != 0)
            {
                _staffing.job_title_id = id;
            }

            id = await GetJobLocationId(data.job_location_name);
            if (id != 0)
            {
                _staffing.job_location_id = id;
            }

            id = await GetClientId(data.client_name);
            if (id != 0)
            {
                _staffing.client_id = id;
            }

            id = await GetAssignedToWhomId(data.assigned_to_whom_name);
            if (id != 0)
            {
                _staffing.assigned_to_whom_id = id;
            }

            id = await GetAccountManagerNameId(data.account_manager_name);
            if (id != 0)
            {
                _staffing.account_manager_id = id;
            }

            id = await GetClientManagerNameId(data.client_manager_name);
            if (id != 0)
            {
                _staffing.client_manager_id = id;
            }

            using (var db = new InternalSystemEntities())
            {
                db.staffings.Add(_staffing);
                int cnt = await db.SaveChangesAsync();
                return _staffing.staffing_id;
            }
        }

        private async Task<int> GetClientManagerNameId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.client_manager.FirstOrDefaultAsync(x => x.client_manager_name == name);
                return (result == null || result.client_manager_id == 0) ? await InsertClientManager(name) : result.client_manager_id;
            }
        }
        private async Task<int> InsertClientManager(string name)
        {
            var newEntry = new client_manager
            {
                client_manager_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.client_manager.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.client_manager_id;
            }
        }

        private async Task<int> GetJobTitleId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.job_title.FirstOrDefaultAsync(x => x.job_title_name == name);
                return (result == null || result.job_title_id == 0) ? await InsertJobTitle(name) : result.job_title_id;
            }
        }
        private async Task<int> InsertJobTitle(string name)
        {
            var newEntry = new job_title
            {
                job_title_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.job_title.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.job_title_id;
            }
        }

        private async Task<int> GetJobLocationId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.job_location.FirstOrDefaultAsync(x => x.job_location_name == name);
                return (result == null || result.job_location_id == 0) ? await InsertJobLocation(name) : result.job_location_id;
            }
        }
        private async Task<int> InsertJobLocation(string name)
        {
            var newEntry = new job_location
            {
                job_location_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.job_location.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.job_location_id;
            }
        }

        private async Task<int> GetClientId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.clients.FirstOrDefaultAsync(x => x.client_name == name);
                return (result == null || result.client_id == 0) ? await InsertClient(name) : result.client_id;
            }
        }
        private async Task<int> InsertClient(string name)
        {
            var newEntry = new client
            {
                client_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.clients.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.client_id;
            }
        }

        private async Task<int> GetAssignedToWhomId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.assigned_to_whom.FirstOrDefaultAsync(x => x.assigned_to_whom_name == name);
                return (result == null || result.assigned_to_whom_id == 0) ? await InsertAssignedToWhom(name) : result.assigned_to_whom_id;
            }
        }
        private async Task<int> InsertAssignedToWhom(string name)
        {
            var newEntry = new assigned_to_whom
            {
                assigned_to_whom_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.assigned_to_whom.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.assigned_to_whom_id;
            }
        }

        private async Task<int> GetAccountManagerNameId(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return 0;
            }

            using (var db = new InternalSystemEntities())
            {
                var result = await db.account_manager.FirstOrDefaultAsync(x => x.account_manager_name == name);
                return (result == null || result.account_manager_id == 0) ? await InsertAccountManagerName(name) : result.account_manager_id;
            }
        }
        private async Task<int> InsertAccountManagerName(string name)
        {
            var newEntry = new account_manager
            {
                account_manager_name = name,
                is_active = true
            };

            using (var db = new InternalSystemEntities())
            {
                db.account_manager.Add(newEntry);
                await db.SaveChangesAsync();
                return newEntry.account_manager_id;
            }
        }

        public async Task<int> Update(int id, StaffingModel data, int adminId, DateTime currentDt)
        {
            using (var db = new InternalSystemEntities())
            {
                var staffing = await db.staffings.FindAsync(id);
                if (staffing == null || staffing.staffing_id == 0)
                {
                    return 0;
                }

                staffing.staffing_id = id;
                staffing.epic_position = data.epic_position;
                staffing.total_no_of_positions = data.total_no_of_positions;
                staffing.no_of_positions_filled = data.no_of_positions_filled;
                staffing.date_job_received = data.date_job_received != null ? data.date_job_received : null;
                staffing.job_description = data.job_description;
                staffing.position_status_id = data.position_status_id;
                staffing.follow_up_action = data.follow_up_action;
                staffing.position_type_id = data.position_type_id;
                staffing.rate_range_w2 = data.rate_range_w2;
                staffing.rate_range_c2c_1099 = data.rate_range_c2c_1099;
                staffing.duration = data.duration;
                staffing.t_e_paid = data.t_e_paid;
                staffing.is_active = true;
                staffing.last_updated_by_user_id = adminId;
                staffing.last_updated_datetime = currentDt;
                staffing.job_closed_date = data.job_closed_date != null ? data.job_closed_date : null;

                int newId = await GetJobTitleId(data.job_title_name);
                if (newId != 0)
                {
                    staffing.job_title_id = newId;
                }

                newId = await GetJobLocationId(data.job_location_name);
                if (newId != 0)
                {
                    staffing.job_location_id = newId;
                }

                newId = await GetClientId(data.client_name);
                if (newId != 0)
                {
                    staffing.client_id = newId;
                }

                newId = await GetAssignedToWhomId(data.assigned_to_whom_name);
                if (newId != 0)
                {
                    staffing.assigned_to_whom_id = newId;
                }

                newId = await GetAccountManagerNameId(data.account_manager_name);
                if (newId != 0)
                {
                    staffing.account_manager_id = newId;
                }

                newId = await GetClientManagerNameId(data.client_manager_name);
                if (id != 0)
                {
                    staffing.client_manager_id = newId;
                }

                await db.SaveChangesAsync();
                return staffing.staffing_id;
            }
        }

        public async Task<bool> Modify(int id, PatchPostModel data, int userId, DateTime currentDt)
        {
            UpdateWhereClause = $"staffing_id = {id}";
            return await ExecUpdate(data, userId, currentDt);
        }

        protected override string TableName()
        {
            return "[dbo].[staffing]";
        }
    }
}
