using Application.DTOs.City;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mapping;

public class CityProfile:Profile
{
    public CityProfile()
    {
        CreateMap<CreateCityRequest, City>();
        CreateMap<City, GetAllCityResponse>().ReverseMap();
        CreateMap<City, GetByIdCityResponse>().ReverseMap();
        CreateMap<UpdateCityRequest, City>().ReverseMap();
    }
}
