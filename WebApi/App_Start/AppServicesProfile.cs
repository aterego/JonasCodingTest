using AutoMapper;
using BusinessLayer.Model.Models;
using WebApi.Models;

namespace WebApi
{
    public class AppServicesProfile : Profile
    {
        public AppServicesProfile()
        {
            CreateMapper();
        }

        //Add .ReverseMap() too
        private void CreateMapper()
        {
            CreateMap<BaseInfo, BaseDto>().ReverseMap();
            CreateMap<CompanyInfo, CompanyDto>().ReverseMap();
            CreateMap<ArSubledgerInfo, ArSubledgerDto>().ReverseMap();
        }
    }
}