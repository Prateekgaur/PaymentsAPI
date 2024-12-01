using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.DTOs;
using PaymentsAPI.Services;

namespace PaymentsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentsController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        [HttpPost]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequestDto request)
        {
            //var result = await _paymentService.InitiatePaymentAsync(request);
            //if (!result.Success)
            //    return BadRequest(result.Message);

            //return Ok(result.PaymentId);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            //var payment = await _paymentService.GetPaymentByIdAsync(id);
            //if (payment == null)
            //    return NotFound();

            //return Ok(payment);
            return Ok();
        }

        [HttpPost("{id}/cancel")]
        public async Task<IActionResult> CancelPayment(int id)
        {
            //var result = await _paymentService.CancelPaymentAsync(id);
            //if (!result.Success)
            //    return BadRequest(result.Message);

            //return Ok("Payment canceled successfully.");
            return Ok();
        }
    }

}
