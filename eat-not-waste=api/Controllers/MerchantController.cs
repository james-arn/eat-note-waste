using eat_not_waste_api.DTOs;
using Microsoft.AspNetCore.Mvc;

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
        var merchant = _merchantService.CreateMerchant(createMerchantDto);
        return CreatedAtAction(nameof(GetMerchantById), new { id = merchant.Id }, merchant);
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
