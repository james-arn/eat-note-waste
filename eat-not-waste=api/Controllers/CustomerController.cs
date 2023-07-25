using eat_not_waste_api.DTOs;
using Microsoft.AspNetCore.Mvc;
using eat_not_waste_api.Services;
using eat_not_waste_api.Enums;
using eat_not_waste_api.Exceptions;
using Serilog;
using Microsoft.AspNetCore.Authentication;

namespace eat_not_waste_api.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;
        private readonly AuthService _authService;

        public CustomerController(CustomerService customerService, AuthService authService)
        {
            _customerService = customerService;
            _authService = authService;
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
        public ActionResult<LoginDto> CreateCustomer(LoginDto createCustomerDto)
        {
            try
            {
                var customer = _authService.RegisterCustomer(createCustomerDto);
                return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
            }
            catch (EmailExistsException ex)
            {
                return Conflict(new { error = ex.ErrorCode, message = ex.Message });
            }
            catch (InvalidEmailException ex)
            {
                return BadRequest(new { error = ex.ErrorCode, message = ex.Message });
            }
            catch (PasswordDoesNotMeetRequirementsException ex)
            {
                return BadRequest(new { error = ex.ErrorCode, message = ex.Message });
            }
            catch (UserAuthenticationFailedException ex)
            {
                return Unauthorized(new { error = ex.ErrorCode, message = ex.Message });
            }
            catch (Exception ex)
            {
                Log.Error(ex, "An unexpected error occurred while trying to create a customer");
                return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
            }
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
