using AutoMapper;
using BusinessLayer.Model.Models;
using System;
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

            // Employee mappings
            CreateMap<EmployeeInfo, EmployeeDto>()
                .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.CompanyCode)) 
                .ForMember(dest => dest.OccupationName, opt => opt.MapFrom(src => src.Occupation))
                .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.LastModifiedDateTime, opt => opt.MapFrom(src => src.LastModified.ToString("o")));
            CreateMap<EmployeeDto, EmployeeInfo>()
                .ForMember(dest => dest.CompanyCode, opt => opt.MapFrom(src => src.CompanyName)) 
                .ForMember(dest => dest.Occupation, opt => opt.MapFrom(src => src.OccupationName))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.LastModified, opt => opt.MapFrom(src => DateTime.Parse(src.LastModifiedDateTime)));
        }
    }
}