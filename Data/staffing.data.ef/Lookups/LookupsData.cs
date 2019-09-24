using staffing.data.models;
using staffing.data.models.Lookups;
using staffing.interfaces.data.Lookups;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace staffing.data.ef.Lookups
{
    public class LookupsData : ILookupsData
    {
        public async Task<List<AccountManagerListModel>> SelectAccountManagers()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.account_manager.Select(x => new AccountManagerListModel()
                {
                    account_manager_id = x.account_manager_id,
                    account_manager_name = x.account_manager_name
                }).ToListAsync();
            }
        }

        public async Task<List<AssignedToWhomListModel>> SelectAssignedToWhoms()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.assigned_to_whom.Select(x => new AssignedToWhomListModel()
                {
                    assigned_to_whom_id = x.assigned_to_whom_id,
                    assigned_to_whom_name = x.assigned_to_whom_name
                }).ToListAsync();
            }
        }

        public async Task<List<ClientListModel>> SelectClients()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.clients.Select(x => new ClientListModel()
                {
                    client_id = x.client_id,
                    client_name = x.client_name
                }).ToListAsync();
            }
        }

        public async Task<List<DropdownListModel>> SelectDropdownList(string search_text, string search_in_master)
        {
            if (string.IsNullOrEmpty(search_in_master))
            {
                return null;
            }

            List<DropdownListModel> resp = new List<DropdownListModel>();
            using (var db = new InternalSystemEntities())
            {
                if (!string.IsNullOrEmpty(search_in_master) && search_in_master == "job_title_name")
                {

                    resp = await (from c in db.job_title
                                  where c.job_title_name.ToLower().Contains(search_text.ToLower())
                                  select new DropdownListModel()
                                  {
                                      dropdown_name = c.job_title_name,
                                      dropdown_id = c.job_title_id
                                  }).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(search_in_master) && search_in_master == "job_location_name")
                {
                    resp = await (from c in db.job_location
                                  where c.job_location_name.ToLower().Contains(search_text.ToLower())
                                  select new DropdownListModel()
                                  {
                                      dropdown_name = c.job_location_name,
                                      dropdown_id = c.job_location_id
                                  }).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(search_in_master) && search_in_master == "client_name")
                {
                    resp = await (from c in db.clients
                                  where c.client_name.ToLower().Contains(search_text.ToLower())
                                  select new DropdownListModel()
                                  {
                                      dropdown_name = c.client_name,
                                      dropdown_id = c.client_id
                                  }).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(search_in_master) && search_in_master == "assigned_to_whom_name")
                {
                    resp = await (from c in db.assigned_to_whom
                                  where c.assigned_to_whom_name.ToLower().Contains(search_text.ToLower())
                                  select new DropdownListModel()
                                  {
                                      dropdown_name = c.assigned_to_whom_name,
                                      dropdown_id = c.assigned_to_whom_id
                                  }).ToListAsync();
                }
                else if (!string.IsNullOrEmpty(search_in_master) && search_in_master == "account_manager_name")
                {
                    resp = await (from c in db.account_manager
                                  where c.account_manager_name.ToLower().Contains(search_text.ToLower())
                                  select new DropdownListModel()
                                  {
                                      dropdown_name = c.account_manager_name,
                                      dropdown_id = c.account_manager_id
                                  }).ToListAsync();
                }
                else if(!string.IsNullOrEmpty(search_in_master) && search_in_master == "client_manager_name")
                {
                    resp = await (from c in db.client_manager
                        where c.client_manager_name.ToLower().Contains(search_text.ToLower())
                        select new DropdownListModel()
                        {
                            dropdown_name = c.client_manager_name,
                            dropdown_id = c.client_manager_id
                        }).ToListAsync();
                }
            }

            return resp;
        }

        public async Task<List<JobLocationListModel>> SelectJobLocations()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.job_location.Select(x => new JobLocationListModel()
                {
                    job_location_id = x.job_location_id,
                    job_location_name = x.job_location_name
                }).ToListAsync();
            }
        }

        public async Task<List<JobTitleListModel>> SelectJobTitles()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.job_title.Select(x => new JobTitleListModel()
                {
                    job_title_id = x.job_title_id,
                    job_title_name = x.job_title_name
                }).ToListAsync();
            }
        }

        public async Task<List<PositionStatusListModel>> SelectPositionStatus()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.position_status.Select(x => new PositionStatusListModel()
                {
                    position_status_id = x.position_status_id,
                    position_status_name = x.position_status_name
                }).ToListAsync();
            }
        }

        public async Task<List<PositionTypeListModel>> SelectPositionTypes()
        {
            using (var db = new InternalSystemEntities())
            {
                return await db.position_type.Select(x => new PositionTypeListModel()
                {
                    position_type_id = x.position_type_id,
                    position_type_name = x.position_type_name
                }).ToListAsync();
            }
        }
    }
}
