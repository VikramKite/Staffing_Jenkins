using staffing.data.models;
using staffing.data.models.Lookups;
using staffing.interfaces.data.Lookups;
using staffing.interfaces.repository.Lookups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace staffing.data.repository.Lookups
{
    public class LookupsRepository : ILookupsRepository
    {
        private readonly ILookupsData _data;

        public LookupsRepository(ILookupsData data)
        {
            _data = data;
        }

        public async Task<List<AccountManagerListModel>> GetAccountManagers()
        {
            return await _data.SelectAccountManagers();
        }

        public async Task<List<AssignedToWhomListModel>> GetAssignedToWhoms()
        {
            return await _data.SelectAssignedToWhoms();
        }

        public async Task<List<ClientListModel>> GetClients()
        {
            return await _data.SelectClients();
        }

        public async Task<List<DropdownListModel>> GetDropdownList(string search_text, string search_in_master)
        {
            return await _data.SelectDropdownList(search_text, search_in_master);
        }

        public async Task<List<JobLocationListModel>> GetJobLocations()
        {
            return await _data.SelectJobLocations();
        }

        public async Task<List<JobTitleListModel>> GetJobTitles()
        {
            return await _data.SelectJobTitles();
        }

        public async Task<List<PositionStatusListModel>> GetPositionStatus()
        {
            return await _data.SelectPositionStatus();
        }

        public async Task<List<PositionTypeListModel>> GetPositionTypes()
        {
            return await _data.SelectPositionTypes();
        }
    }
}
