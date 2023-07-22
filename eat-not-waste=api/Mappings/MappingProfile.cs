using AutoMapper;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>();
            CreateMap<Customer, CreateCustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();

            CreateMap<Listing, ListingDto>();
            CreateMap<ListingDto, Listing>();
            CreateMap<Listing, CreateListingDto>();
            CreateMap<CreateListingDto, Listing>();

            CreateMap<Merchant, MerchantDto>();
            CreateMap<MerchantDto, Merchant>();
            CreateMap<Merchant, CreateMerchantDto>();
            CreateMap<CreateMerchantDto, Merchant>();

            CreateMap<Purchase, PurchaseDto>();
            CreateMap<PurchaseDto, Purchase>();
            CreateMap<Purchase, CreatePurchaseDto>();
            CreateMap<CreatePurchaseDto, Purchase>();

            CreateMap<Review, ReviewDto>();
            CreateMap<ReviewDto, Review>();
            CreateMap<Review, CreateReviewDto>();
            CreateMap<CreateReviewDto, Review>();
        }
    }

}
