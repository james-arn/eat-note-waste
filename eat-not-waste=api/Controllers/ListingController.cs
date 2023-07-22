using eat_not_waste_api.DTOs;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ListingController : ControllerBase
{
    private readonly ListingService _listingService;

    public ListingController(ListingService listingService)
    {
        _listingService = listingService;
    }

    // GET api/listing
    [HttpGet]
    public ActionResult<IEnumerable<ListingDto>> GetListings([FromQuery] string location)
    {
        return Ok(_listingService.GetAllListings(location));
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
        var listing = _listingService.CreateListing(createListingDto);
        return CreatedAtAction(nameof(GetListing), new { id = listing.Id }, listing);
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
