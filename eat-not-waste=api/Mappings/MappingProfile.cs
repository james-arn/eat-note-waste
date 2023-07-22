using AutoMapper;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<Customer, CustomerDto>();
            // CreateMap<CustomerDto, Customer>();

            CreateMap<Listing, ListingDto>();
            CreateMap<ListingDto, Listing>();

            CreateMap<Merchant, MerchantDto>();
            CreateMap<MerchantDto, Merchant>();

            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
        }
    }

}
