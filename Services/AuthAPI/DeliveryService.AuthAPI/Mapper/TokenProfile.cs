using AutoMapper;
using DeliveryService.AuthAPI.Model;
using DeliveryService.AuthAPI.Model.Responses;

namespace DeliveryService.AuthAPI.Mapper
{
    public class TokenProfile : Profile
    {
        public TokenProfile()
        {
            CreateMap<TokenResponse, TokenModel>().ReverseMap();
        }
    }
}
