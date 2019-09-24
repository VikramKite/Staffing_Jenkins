using staffing.data.models;
using System.Collections.Generic;
using System.Threading.Tasks;
using staffing.data.models.Lookups;

namespace staffing.interfaces.data.Lookups
{
    public interface ILookupsData
    {
        Task<List<JobTitleListModel>> SelectJobTitles();
        Task<List<PositionStatusListModel>> SelectPositionStatus();
        Task<List<JobLocationListModel>> SelectJobLocations();
        Task<List<AssignedToWhomListModel>> SelectAssignedToWhoms();
        Task<List<AccountManagerListModel>> SelectAccountManagers();
        Task<List<ClientListModel>> SelectClients();
        Task<List<PositionTypeListModel>> SelectPositionTypes();

        //
        Task<List<DropdownListModel>> SelectDropdownList(string search_text,string search_in_master);
    }
}
