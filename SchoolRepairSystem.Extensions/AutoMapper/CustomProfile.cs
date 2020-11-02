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
            CreateMap<ReportForRepair, RoleRepairViewModel>();
            //.ForMember(desc=>desc.Layer,opt=>opt.MapFrom(src=>src.Layer))
            //.ForMember(desc => desc.Tung, opt => opt.MapFrom(src => src.Tung))
            //.ForMember(desc => desc.Dorm, opt => opt.MapFrom(src => src.Dorm))
            //.ForMember(desc => desc.Desc, opt => opt.MapFrom(src => src.Desc))
            //.ForMember(desc => desc.Evaluate, opt => opt.MapFrom(src => src.Evaluate))
            //.ForMember(desc => desc.WaitHandle, opt => opt.MapFrom(src => src.WaitHandle))
            //.ForMember(desc => desc.DateTime, opt => opt.MapFrom(src => src.DateTime));

        }
    }
}