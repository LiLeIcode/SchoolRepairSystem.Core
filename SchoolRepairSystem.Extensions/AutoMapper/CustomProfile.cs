using AutoMapper;
using SchoolRepairSystem.Models;
using SchoolRepairSystem.Models.ViewModels;

namespace SchoolRepairSystem.Extensions.AutoMapper
{
    public class CustomProfile:Profile
    {
        public CustomProfile()
        {
            CreateMap<Users, UserViewModel>();
            CreateMap<ReportForRepair, ReportForRepairRecordViewModel>().ForMember(desc=>desc.ReportDateTime,opt=>opt.MapFrom(src=>src.DateTime));
            CreateMap<WareHouse,GoodsInfoViewModel>();
            CreateMap<ReportForRepair, ReportForRepairViewModel>();

        }
    }
}