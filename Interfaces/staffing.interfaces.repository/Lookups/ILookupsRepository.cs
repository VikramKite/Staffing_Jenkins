using staffing.data.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using staffing.data.models.Lookups;

namespace staffing.interfaces.repository.Lookups
{
    public interface ILookupsRepository
    {
        Task<List<JobTitleListModel>> GetJobTitles();
        Task<List<PositionStatusListModel>> GetPositionStatus();
        Task<List<JobLocationListModel>> GetJobLocations();
        Task<List<AssignedToWhomListModel>> GetAssignedToWhoms();
        Task<List<AccountManagerListModel>> GetAccountManagers();
        Task<List<ClientListModel>> GetClients();
        Task<List<PositionTypeListModel>> GetPositionTypes();
        Task<List<DropdownListModel>> GetDropdownList(string search_text, string search_in_master);
    }
}
