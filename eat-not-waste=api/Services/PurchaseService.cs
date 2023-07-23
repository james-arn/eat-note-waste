using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Exceptions;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Services
{
    public class PurchaseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PurchaseService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<PurchaseDto> GetAllPurchases()
        {
            var purchases = _context.Purchases.ToList();
            return _mapper.Map<List<PurchaseDto>>(purchases);
        }

        public PurchaseDto GetPurchaseById(int id)
        {
            var purchase = _context.Purchases.Find(id);
            return purchase == null ? null : _mapper.Map<PurchaseDto>(purchase);
        }

        public PurchaseDto CreatePurchase(CreatePurchaseDto createPurchaseDto)
        {
            // Check customer can be found
            var customer = _context.Customers.Find(createPurchaseDto.CustomerId);
            if (customer == null)
            {
                throw new ArgumentException("Customer not found");
            }

            // Check listing can be found
            var listing = _context.Listings.Find(createPurchaseDto.ListingId);
            if (listing == null)
            {
                throw new ArgumentException("Listing not found");
            }
            // Check if the listing has expired, if yes, don't allow purchase.
            if (DateTime.Now > listing.ExpirationDate)
            {
                throw new ListingExpiredException();
            }
            // Check in stock
            if (listing.Quantity < createPurchaseDto.Quantity)
            {
                throw new OutOfStockException();
            }

            // Decrement the quantity of the listing
            listing.Quantity -= createPurchaseDto.Quantity;

            // Proceed with purchase
            var purchase = _mapper.Map<Purchase>(createPurchaseDto);
            purchase.PurchaseDate = DateTime.UtcNow;
            _context.Purchases.Add(purchase);
            _context.SaveChanges();
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public PurchaseDto UpdatePurchase(int id, PurchaseDto purchaseDto)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase == null)
            {
                return null;
            }
            _mapper.Map(purchaseDto, purchase);
            _context.SaveChanges();
            return _mapper.Map<PurchaseDto>(purchase);
        }

        public void DeletePurchase(int id)
        {
            var purchase = _context.Purchases.Find(id);
            if (purchase != null)
            {
                _context.Purchases.Remove(purchase);
                _context.SaveChanges();
            }
        }
    }
}
