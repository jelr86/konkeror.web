using AutoMapper;
using konkeror.app.Models;
using konkeror.data.Domain;

namespace konkeror.web.Infrastructure
{
    public class KonkerorAutoMapperProfile : Profile
    {
        public KonkerorAutoMapperProfile()
        {
            CreateMap<Client, ClientModel>().ReverseMap();
            CreateMap<Client, CreateClientModel>().ReverseMap();
        }
    }
}