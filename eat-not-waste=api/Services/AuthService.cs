using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Exceptions;
using eat_not_waste_api.Helpers;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly JwtAuthService _jwtAuthService;

        public AuthService(ApplicationDbContext context, IMapper mapper, JwtAuthService jwtAuthService)
        {
            _context = context;
            _mapper = mapper;
            _jwtAuthService = jwtAuthService;
        }

        public CustomerDto RegisterCustomer(LoginDto createCustomerDto)
        {
                // Check if email is valid
                if (!UserRegisterLoginService.IsValidEmail(createCustomerDto.Email))
                {
                    throw new InvalidEmailException();
                }

                // Check if email is unique
                var existingCustomer = _context.Customers.SingleOrDefault(c => c.Email == createCustomerDto.Email);
                if (existingCustomer != null)
                {
                    throw new EmailExistsException();
                }

                // Check if password meets complexity requirements
                if (!UserRegisterLoginService.PasswordMeetsRequirements(createCustomerDto.Password))
                {
                    throw new PasswordDoesNotMeetRequirementsException();
                }

                var customer = _mapper.Map<Customer>(createCustomerDto);
                _context.Customers.Add(customer);
                _context.SaveChanges();

                // Refetch the customer object from the database
                var customerEntity = _context.Customers.Find(customer.Id);

                return _mapper.Map<CustomerDto>(customerEntity);
        }

        public string LoginCustomer(LoginDto loginDto)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Email == loginDto.Email);
            if (customer == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, customer.Password))
            {
                throw new UserAuthenticationFailedException();
            }

            return _jwtAuthService.GenerateToken(customer.Id.ToString());
        }

        public MerchantDto RegisterMerchant(CreateMerchantDto createMerchantDto)
        {
            // Check if email is unique
            var existingMerchant = _context.Merchants.SingleOrDefault(m => m.Email == createMerchantDto.Email);
            if (existingMerchant != null)
            {
                throw new EmailExistsException();
            }

            // Check if email is valid
            if (!UserRegisterLoginService.IsValidEmail(createMerchantDto.Email))
            {
                throw new InvalidEmailException();
            }

            // Check if password meets complexity requirements
            if (!UserRegisterLoginService.PasswordMeetsRequirements(createMerchantDto.Password))
            {
                throw new PasswordDoesNotMeetRequirementsException();
            }
            var merchant = _mapper.Map<Merchant>(createMerchantDto);
            _context.Merchants.Add(merchant);
            _context.SaveChanges();

            // Refetch the merchant object from the database
            var merchantEntity = _context.Merchants.Find(merchant.Id);

            return _mapper.Map<MerchantDto>(merchantEntity);
        }

        public string LoginMerchant(LoginDto loginDto)
        {
            var merchant = _context.Merchants.SingleOrDefault(m => m.Email == loginDto.Email);
            if (merchant == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, merchant.Password))
            {
                throw new UserAuthenticationFailedException();
            }

            return _jwtAuthService.GenerateToken(merchant.Id.ToString());
        }
    }
}
