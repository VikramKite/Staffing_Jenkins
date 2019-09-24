using staffing.data.models;
using staffing.data.models.Common;
using staffing.data.models.Common.Post;
using staffing.data.models.Staffing;
using staffing.interfaces.processor.Staffing;
using staffing.interfaces.repository.Staffing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace staffing.processor.webadmin.Staffing
{
    public class StaffingProcessor: IStaffingProcessor
    {
        private readonly IStaffingRepository _repository;
        public StaffingProcessor(IStaffingRepository repository)
        {
            _repository = repository;
        }

        public async Task<ResponseDataModel<List<StaffingListModel>>> GetAll()
        {
            return new ResponseDataModel<List<StaffingListModel>> { data = await _repository.GetAll() };
        }

        public async Task<ResponseDataModel<StaffingModel>> GetById(int id)
        {
            if (id == 0)
            {
                return new ResponseDataModel<StaffingModel> { success = false, message = "No Staffing Id provided", data = null };
            }

            return new ResponseDataModel<StaffingModel> { data = await _repository.GetById(id) };
        }

        public async Task<ResponseDataModel<List<StaffingListModel>>> GetStaffingListItems(DateTime currentDt)
        {
            return new ResponseDataModel<List<StaffingListModel>> { data = await _repository.GetStaffingListItems(currentDt) };
        }

        public async Task<ResponseDataModel<int>> Insert(StaffingModel data, int adminId, DateTime currentDt)
        {
            return new ResponseDataModel<int> { data = await _repository.Insert(data, adminId, currentDt) };
        }

        public async Task<ResponseModel> Modify(int id, PatchPostModel data, int adminId, DateTime currentDt)
        {
            if (data == null)
            {
                return new ResponseDataModel<int> { success = false, data = 0, message = "No data provided" };
            }

            if (adminId == 0)
            {
                return new ResponseModel { success = false, message = "No Administrator loogged in" };
            }

            return await _repository.Modify(id, data, adminId, currentDt) ? new ResponseModel() : new ResponseModel { success = false, message = "An error saving data occurred" };
        }

        public async Task<ResponseDataModel<int>> Update(int id, StaffingModel data, int adminId, DateTime currentDt)
        {
            return new ResponseDataModel<int> { data = await _repository.Update(id, data, adminId, currentDt) };
        }
    }
}
