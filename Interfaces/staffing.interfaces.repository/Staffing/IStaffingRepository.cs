using staffing.data.models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using staffing.data.models.Staffing;
using staffing.data.models.Common.Post;

namespace staffing.interfaces.repository.Staffing
{
    public interface IStaffingRepository
    {
        Task<StaffingModel> GetById(int id);
        Task<List<StaffingListModel>> GetAll();
        Task<int> Insert(StaffingModel data, int adminId, DateTime currentDt);
        Task<int> Update(int id, StaffingModel data, int adminId, DateTime currentDt);
        Task<List<StaffingListModel>> GetStaffingListItems(DateTime currentDt);
        Task<bool> Modify(int id, PatchPostModel data, int adminId, DateTime currentDt);
    }
}
