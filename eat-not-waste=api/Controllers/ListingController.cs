using eat_not_waste_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Route("api/[controller]")]
[ApiController]
public class ListingController : ControllerBase
{
    private readonly ListingService _listingService;

    public ListingController(ListingService listingService)
    {
        _listingService = listingService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ListingDto>> GetAllListings(
        string location = null,
        bool includeExpired = false,
        bool includeOutOfStock = false)
    {
        var listings = _listingService.GetAllListings(location, includeExpired, includeOutOfStock);
        return Ok(listings);
    }


    // GET api/listing/{id}
    [HttpGet("{id}")]
    public ActionResult<ListingDto> GetListing(int id)
    {
        var listing = _listingService.GetListingById(id);
        if (listing == null)
        {
            return NotFound();
        }
        return Ok(listing);
    }

    // POST api/listing
    [HttpPost]
    public ActionResult<ListingDto> CreateListing(CreateListingDto createListingDto)
    {
        try
        {
            var listing = _listingService.CreateListing(createListingDto);
            return CreatedAtAction(nameof(GetListing), new { id = listing.Id }, listing);
        }
        catch (ArgumentException ex)
        {
            Log.Error(ex, "Failed to create listing with details: {@createListingDto}", createListingDto);
            return BadRequest(ex.Message);
        }
    }

    // PUT api/listing/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateListing(int id, ListingDto listingDto)
    {
        var listing = _listingService.UpdateListing(id, listingDto);
        if (listing == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    // DELETE api/listing/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteListing(int id)
    {
        var listing = _listingService.DeleteListing(id);
        if (listing == null)
        {
            return NotFound();
        }
        return NoContent();
    }
}
