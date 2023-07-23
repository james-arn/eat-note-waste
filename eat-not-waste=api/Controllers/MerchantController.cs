using eat_not_waste_api.DTOs;
using eat_not_waste_api.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Serilog;

[Route("api/[controller]")]
[ApiController]
public class MerchantController : ControllerBase
{
    private readonly MerchantService _merchantService;

    public MerchantController(MerchantService merchantService)
    {
        _merchantService = merchantService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<MerchantDto>> GetAllMerchants()
    {
        return Ok(_merchantService.GetAllMerchants());
    }

    [HttpGet("{id}")]
    public ActionResult<MerchantDto> GetMerchantById(int id)
    {
        var merchant = _merchantService.GetMerchantById(id);
        if (merchant == null)
        {
            return NotFound();
        }
        return Ok(merchant);
    }

    [HttpPost]
    public ActionResult<MerchantDto> CreateMerchant(CreateMerchantDto createMerchantDto)
    {
        try
        {
            var merchant = _merchantService.CreateMerchant(createMerchantDto);
            return CreatedAtAction(nameof(GetMerchantById), new { id = merchant.Id }, merchant);
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

    [HttpPut("{id}")]
    public IActionResult UpdateMerchant(int id, MerchantDto merchantDto)
    {
        var merchant = _merchantService.UpdateMerchant(id, merchantDto);
        if (merchant == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteMerchant(int id)
    {
        var wasDeleted = _merchantService.DeleteMerchant(id);
        if (!wasDeleted)
        {
            return NotFound();
        }
        return NoContent();
    }
}
