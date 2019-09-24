using staffing.data.models;
using staffing.data.models.Common.Post;
using staffing.data.models.Staffing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace staffing.interfaces.data.Staffing
{
    public interface IStaffingData
    {
        Task<StaffingModel> GetById(int id);
        Task<List<StaffingListModel>> GetAll();
        Task<int> Insert(StaffingModel data, int adminId, DateTime currentDt);
        Task<int> Update(int id, StaffingModel data, int adminId, DateTime currentDt);
        Task<bool> Modify(int id, PatchPostModel data, int userId, DateTime currentDt);
    }
}
