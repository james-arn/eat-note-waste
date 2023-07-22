using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Services
{
    public class ReviewService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ReviewService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public ReviewDto CreateReview(CreateReviewDto createReviewDto)
        {
            if (createReviewDto.Rating < 1 || createReviewDto.Rating > 5)
            {
                throw new ArgumentException("Rating must be between 1 and 5");
            }

            var review = _mapper.Map<Review>(createReviewDto);
            _context.Reviews.Add(review);
            _context.SaveChanges();

            // Refetch the review object from the database
            var reviewEntity = _context.Reviews.Find(review.Id);

            return _mapper.Map<ReviewDto>(reviewEntity);
        }

        public List<ReviewDto> GetAllReviews()
        {
            var reviews = _context.Reviews.ToList();
            return _mapper.Map<List<ReviewDto>>(reviews);
        }

        public ReviewDto GetReviewById(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return null;
            }
            return _mapper.Map<ReviewDto>(review);
        }

        public ReviewDto UpdateReview(int id, ReviewDto reviewDto)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return null;
            }

            _mapper.Map(reviewDto, review);
            _context.SaveChanges();

            return _mapper.Map<ReviewDto>(review);
        }

        public bool DeleteReview(int id)
        {
            var review = _context.Reviews.Find(id);
            if (review == null)
            {
                return false;
            }

            _context.Reviews.Remove(review);
            _context.SaveChanges();

            return true;
        }


    }

}