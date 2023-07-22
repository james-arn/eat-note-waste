using eat_not_waste_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using eat_not_waste_api.Services;

namespace eat_not_waste_api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        // GET api/customers
        [HttpGet]
        public ActionResult<IEnumerable<CustomerDto>> GetCustomers()
        {
            return Ok(_customerService.GetAllCustomers());
        }

        // GET api/customers/{id}
        [HttpGet("{id}")]
        public ActionResult<CustomerDto> GetCustomer(int id)
        {
            var customer = _customerService.GetCustomerById(id);
            if (customer == null)
            {
                return NotFound();
            }
            return Ok(customer);
        }

        // POST api/customer
        [HttpPost]
        public ActionResult<CreateCustomerDto> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var customer = _customerService.CreateCustomer(createCustomerDto);
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT api/customers/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateCustomer(int id, CustomerDto customer)
        {
            var existingCustomer = _customerService.UpdateCustomer(id, customer);
            if (existingCustomer == null)
            {
                return NotFound();
            }
            return NoContent();
        }

        // DELETE api/customers/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteCustomer(int id)
        {
            var customer = _customerService.DeleteCustomer(id);
            if (customer == null)
            {
                return NotFound();
            }
            return NoContent();
        }
    }

}
