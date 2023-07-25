using Microsoft.AspNetCore.Mvc;
using eat_not_waste_api.Services;
using eat_not_waste_api.Exceptions;
using Serilog;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    // POST api/authentication/logincustomer
    [HttpPost("logincustomer")]
    public ActionResult<string> LoginCustomer(LoginDto loginDto)
    {
        try
        {
            var token = _authService.LoginCustomer(loginDto);
            return Ok(new { token = token });
        }
        catch (UserAuthenticationFailedException ex)
        {
            return Unauthorized(new { error = ex.ErrorCode, message = ex.Message });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred while trying to log in a customer");
            return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
        }
    }

    // POST api/authentication/loginmerchant
    [HttpPost("loginmerchant")]
    public ActionResult<string> LoginMerchant(LoginDto loginDto)
    {
        try
        {
            var token = _authService.LoginMerchant(loginDto);
            return Ok(new { token = token });
        }
        catch (UserAuthenticationFailedException ex)
        {
            return Unauthorized(new { error = ex.ErrorCode, message = ex.Message });
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An unexpected error occurred while trying to log in a merchant");
            return StatusCode(500, new { message = "An unexpected error occurred. Please try again later." });
        }
    }
}
