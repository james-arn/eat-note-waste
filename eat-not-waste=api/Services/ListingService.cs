using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;

public class ListingService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ListingService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public List<ListingDto> GetAllListings(string location = null, bool includeExpired = false, bool includeOutOfStock = false)
    {
        var listings = _context.Listings.AsQueryable();

        if (!string.IsNullOrEmpty(location))
        {
            listings = listings.Where(l => l.Location == location);
        }

        if (!includeExpired)
        {
            listings = listings.Where(l => l.ExpirationDate > DateTime.Now);
        }

        if (!includeOutOfStock)
        {
            listings = listings.Where(l => l.Quantity > 0);
        }

        return _mapper.Map<List<ListingDto>>(listings.ToList());
    }


    public ListingDto GetListingById(int id)
    {
        var listing = _context.Listings.Find(id);
        return _mapper.Map<ListingDto>(listing);
    }

    public ListingDto CreateListing(CreateListingDto createListingDto)
    {
        // Check merhant exists that's creating the listing
        var merchant = _context.Customers.Find(createListingDto.MerchantId);
        if (merchant == null)
        {
            throw new ArgumentException("Customer not found");
        }

        // Ensure quantity ad price are a range
        if (createListingDto.Quantity <= 0 || createListingDto.Quantity > 50)
        {
            throw new ArgumentException("Quantity must be between 1 and 50.");
        }

        if (createListingDto.Price <= 0 || createListingDto.Price > 20)
        {
            throw new ArgumentException("Price must be between 0.01 and 20.00.");
        }

        // Proceed creating listing
        var listing = _mapper.Map<Listing>(createListingDto);
        _context.Listings.Add(listing);
        _context.SaveChanges();

        return _mapper.Map<ListingDto>(listing);
    }

    public ListingDto UpdateListing(int id, ListingDto listingDto)
    {
        var listing = _context.Listings.Find(id);
        if (listing == null)
        {
            return null;
        }

        _mapper.Map(listingDto, listing);
        _context.SaveChanges();

        return _mapper.Map<ListingDto>(listing);
    }

    public ListingDto DeleteListing(int id)
    {
        var listing = _context.Listings.Find(id);
        if (listing == null)
        {
            return null;
        }

        var listingDto = _mapper.Map<ListingDto>(listing);

        _context.Listings.Remove(listing);
        _context.SaveChanges();

        return listingDto;
    }
}
