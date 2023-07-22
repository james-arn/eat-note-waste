using AutoMapper;
using eat_not_waste_api.Data;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;

namespace eat_not_waste_api.Services
{
    public class CustomerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<CustomerDto> GetAllCustomers()
        {
            var customers = _context.Customers.ToList();
            return _mapper.Map<List<CustomerDto>>(customers);
        }

        public CustomerDto GetCustomerById(int id)
        {
            var customer = _context.Customers.Find(id);
            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = _mapper.Map<Customer>(createCustomerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            // Refetch the customer object from the database
            var customerEntity = _context.Customers.Find(customer.Id);

            return _mapper.Map<CustomerDto>(customerEntity);
        }

        public CustomerDto UpdateCustomer(int id, CustomerDto customerDto)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return null;
            }

            _mapper.Map(customerDto, customer);
            _context.SaveChanges();

            return _mapper.Map<CustomerDto>(customer);
        }

        public CustomerDto DeleteCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
            {
                return null;
            }

            _context.Customers.Remove(customer);
            _context.SaveChanges();

            return _mapper.Map<CustomerDto>(customer);
        }
    }


}