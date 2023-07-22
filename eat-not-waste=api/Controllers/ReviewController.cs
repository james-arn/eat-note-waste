using eat_not_waste_api.DTOs;
using eat_not_waste_api.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ReviewController : ControllerBase
{
    private readonly ReviewService _reviewService;

    public ReviewController(ReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReviewDto>> GetAllReviews()
    {
        return Ok(_reviewService.GetAllReviews());
    }

    [HttpGet("{id}")]
    public ActionResult<ReviewDto> GetReviewById(int id)
    {
        var review = _reviewService.GetReviewById(id);
        if (review == null)
        {
            return NotFound();
        }
        return Ok(review);
    }

    [HttpPost]
    public ActionResult<ReviewDto> CreateReview(CreateReviewDto createReviewDto)
    {
        var review = _reviewService.CreateReview(createReviewDto);
        return CreatedAtAction(nameof(GetReviewById), new { id = review.Id }, review);
    }

    [HttpPut("{id}")]
    public IActionResult UpdateReview(int id, ReviewDto reviewDto)
    {
        var review = _reviewService.UpdateReview(id, reviewDto);
        if (review == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteReview(int id)
    {
        var wasDeleted = _reviewService.DeleteReview(id);
        if (!wasDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
