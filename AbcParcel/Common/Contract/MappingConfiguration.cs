using AbcParcel.Data;
using AbcParcel.Models;
using AutoMapper;

namespace AbcParcel.Common.Contract
{
    public class MappingConfiguration : Profile
    {
        public MappingConfiguration()
        {
            CreateMap<ParcelViewModel, Parcel>().ReverseMap();
            CreateMap<RegisterAdminViewModel, Applicationuser>().ReverseMap();
        }
    }
}
