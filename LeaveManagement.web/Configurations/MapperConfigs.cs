using AutoMapper;
using LeaveManagement.web.Model;
using LeaveManagement.web.ViewModels;

namespace LeaveManagement.web.Configurations
{
    public class MapperConfigs:Profile
    {
        public MapperConfigs()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
        }
    }
}
