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

    public List<ListingDto> GetAllListings(string location = null)
    {
        var listings = string.IsNullOrEmpty(location)
            ? _context.Listings.ToList()
            : _context.Listings.Where(l => l.Location == location).ToList();

        return _mapper.Map<List<ListingDto>>(listings);
    }

    public ListingDto GetListingById(int id)
    {
        var listing = _context.Listings.Find(id);
        return _mapper.Map<ListingDto>(listing);
    }

    public ListingDto CreateListing(CreateListingDto createListingDto)
    {
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
