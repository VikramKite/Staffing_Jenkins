using staffing.data.models;
using staffing.data.models.Lookups;
using staffing.interfaces.processor.Lookups;
using staffing.interfaces.repository.Lookups;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace staffing.processor.webadmin.Lookups
{
    public class LookupsProcessor : ILookupsProcessor
    {
        private readonly ILookupsRepository _repository;

        public LookupsProcessor(ILookupsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<AccountManagerListModel>> GetAccountManagers()
        {
            return await _repository.GetAccountManagers();
        }

        public async Task<List<AssignedToWhomListModel>> GetAssignedToWhoms()
        {
            return await _repository.GetAssignedToWhoms();
        }

        public async Task<List<ClientListModel>> GetClients()
        {
            return await _repository.GetClients();
        }

        public async Task<List<DropdownListModel>> GetDropdownList(string search_text, string search_in_master)
        {
            return await _repository.GetDropdownList(search_text,search_in_master);
        }

        public async Task<List<JobLocationListModel>> GetJobLocations()
        {
            return await _repository.GetJobLocations();
        }

        public async Task<List<JobTitleListModel>> GetJobTitles()
        {
            return await _repository.GetJobTitles();
        }

        public async Task<List<PositionStatusListModel>> GetPositionStatus()
        {
            return await _repository.GetPositionStatus();
        }

        public async Task<List<PositionTypeListModel>> GetPositionTypes()
        {
            return await _repository.GetPositionTypes();
        }
    }
}
