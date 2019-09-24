using staffing.data.models.Common;
using System;
using System.Threading.Tasks;
using staffing.data.models;
using System.Collections.Generic;
using staffing.data.models.Staffing;
using staffing.data.models.Common.Post;

namespace staffing.interfaces.processor.Staffing
{
    public interface IStaffingProcessor
    {
        Task<ResponseDataModel<StaffingModel>> GetById(int id);
        Task<ResponseDataModel<List<StaffingListModel>>> GetAll();
        Task<ResponseDataModel<int>> Insert(StaffingModel data, int adminId, DateTime currentDt);
        Task<ResponseDataModel<int>> Update(int id, StaffingModel data, int adminId, DateTime currentDt);
        Task<ResponseDataModel<List<StaffingListModel>>> GetStaffingListItems(DateTime currentDt);
        Task<ResponseModel> Modify(int id, PatchPostModel data, int adminId, DateTime currentDt);
    }
}
