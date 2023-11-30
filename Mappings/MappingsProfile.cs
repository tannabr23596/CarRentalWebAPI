using abraham_luzon_group6_assignment.DTOs;
using AutoMapper;
using CarRentalLibrary.Models;
using abraham_luzon_group6_assignment.DTOs;

namespace abraham_luzon_group6_assignment.Mappings
{
    public class MappingsProfile : Profile
    {
       public MappingsProfile() {
            CreateMap<Carrental, CarRentalDto>() ;
            CreateMap<CarRentalDto, Carrental>();
            CreateMap<CarDto, Car>();
            CreateMap<Car, CarDto>();
            CreateMap<BookingDto, Booking>();
            CreateMap<Booking, BookingDto>();


        }
    }
}
