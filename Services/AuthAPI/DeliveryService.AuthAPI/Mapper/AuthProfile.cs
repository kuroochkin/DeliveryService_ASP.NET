using AutoMapper;
using DeliveryService.AuthAPI.Model.Responses;
using DeliveryService.AuthAPI.Services;

namespace DeliveryService.AuthAPI.Mapper
{
    public class AuthProfile : Profile
    {
        public AuthProfile()
        {
            CreateMap<AuthResponse, ResultService>().ReverseMap();
        }
    }
}
