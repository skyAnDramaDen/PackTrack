using AbcParcel.Data;
using AbcParcel.Models;
using AutoMapper;

namespace AbcParcel.Common.Contract
{
    // Handles the mapping configuration between view models and domain models using AutoMapper.
    public class MappingConfiguration : Profile
    {
        // Constructor for MappingConfiguration class.
        public MappingConfiguration()
        {
            // Define mapping between ParcelViewModel and Parcel model, allowing bi-directional mapping.
            CreateMap<ParcelViewModel, Parcel>().ReverseMap();
            // Define mapping between RegisterAdminViewModel and Applicationuser model, allowing bi-directional mapping.
            CreateMap<RegisterAdminViewModel, Applicationuser>().ReverseMap();
        }
    }
}
