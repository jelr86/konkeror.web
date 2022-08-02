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

            CreateMap<License, LicenseModel>().ReverseMap();
            CreateMap<License, CreateLicenseModel>().ReverseMap();
            CreateMap<License, CreateLicenseResultModel>().ReverseMap();

            CreateMap<Product, ProductModel>().ReverseMap();
            CreateMap<Product, CreateProductModel>().ReverseMap();

            CreateMap<Transaction, TransactionModel>().ReverseMap();
        }
    }
}