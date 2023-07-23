using Microsoft.AspNetCore.Mvc;
using eat_not_waste_api.Services;
using eat_not_waste_api.DTOs;
using eat_not_waste_api.Models;
using Serilog;

namespace eat_not_waste_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly PurchaseService _purchaseService;

        public PurchaseController(PurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        // GET: api/purchase
        [HttpGet]
        public IActionResult GetAllPurchases()
        {
            var purchases = _purchaseService.GetAllPurchases();
            return Ok(purchases);
        }

        // GET: api/purchase/{id}
        [HttpGet("{id}")]
        public IActionResult GetPurchaseById(int id)
        {
            var purchase = _purchaseService.GetPurchaseById(id);
            return purchase == null ? NotFound() : Ok(purchase);
        }

        // POST: api/purchase
        [HttpPost]
        public IActionResult CreatePurchase([FromBody] CreatePurchaseDto createPurchaseDto)
        {
            try
            {
                var purchase = _purchaseService.CreatePurchase(createPurchaseDto);
                return CreatedAtAction(nameof(GetPurchaseById), new { id = purchase.Id }, purchase);
            }
            catch (ArgumentException exception)
            {
                Log.Error(exception, "Failed to create listing with details: {@createPurchaseDto}", createPurchaseDto);
                return NotFound(new { message = exception.Message });
            }
            catch (InvalidOperationException exception)
            {
                Log.Error(exception, "Failed to create listing with details: {@createPurchaseDto}", createPurchaseDto);
                return BadRequest(new { message = exception.Message });
            }
        }

        // PUT: api/purchase/{id}
        [HttpPut("{id}")]
        public IActionResult UpdatePurchase(int id, [FromBody] PurchaseDto purchaseDto)
        {
            var purchase = _purchaseService.UpdatePurchase(id, purchaseDto);
            return purchase == null ? NotFound() : NoContent();
        }

        // DELETE: api/purchase/{id}
        [HttpDelete("{id}")]
        public IActionResult DeletePurchase(int id)
        {
            _purchaseService.DeletePurchase(id);
            return NoContent();
        }
    }
}
