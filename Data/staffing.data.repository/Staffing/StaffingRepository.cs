using staffing.data.models;
using staffing.data.models.Common.Post;
using staffing.data.models.Staffing;
using staffing.interfaces.data.Staffing;
using staffing.interfaces.repository.Staffing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace staffing.data.repository.Staffing
{
    public class StaffingRepository : IStaffingRepository
    {
        private readonly IStaffingData _data;
        public StaffingRepository(IStaffingData data)
        {
            _data = data;
        }
        public async Task<List<StaffingListModel>> GetAll()
        {
            return await _data.GetAll();
        }

        public async Task<StaffingModel> GetById(int id)
        {
            return await _data.GetById(id);
        }

        public  Task<List<StaffingListModel>> GetStaffingListItems(DateTime currentDt)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Insert(StaffingModel data, int adminId, DateTime currentDt)
        {
            return await _data.Insert(data, adminId, currentDt);
        }

        public async Task<bool> Modify(int id, PatchPostModel data, int userId, DateTime currentDt)
        {
            return await _data.Modify(id, data, userId, currentDt);
        }

        public async Task<int> Update(int id, StaffingModel data, int adminId, DateTime currentDt)
        {
            return await _data.Update(id, data, adminId, currentDt);
        }
    }
}
