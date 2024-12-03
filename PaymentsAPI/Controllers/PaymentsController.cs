using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PaymentsAPI.DTOs;
using PaymentsAPI.Models;
using PaymentsAPI.Services;

namespace PaymentsAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        private readonly IUserService _userService;
        private readonly CurrentUserService _currentUserService;

        public PaymentsController(IPaymentService paymentService, IUserService userService, CurrentUserService currentUserService)
        {
            _paymentService = paymentService;
            _userService = userService;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// get payment details by paymentId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
                return NotFound();

            return Ok(payment);
        }

        /// <summary>
        /// initiate payment
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> InitiatePayment([FromBody] PaymentRequestDto request)
        {
            int userId = Int32.Parse(_currentUserService.UserId);
            if (request is null || request.Amount <= 0 || string.IsNullOrEmpty(request.PaymentMethod) || request.RecipientId <= 0 || request.RecipientId == userId)
            {
                return BadRequest("invalid request");
            }
            Payment payment = new Payment()
            {
                UserId = userId,
                Amount = request.Amount,
                RecipientId = request.RecipientId,
                PaymentMethod = request.PaymentMethod,
            };
            var result = await _paymentService.InitiatePaymentAsync(payment);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.PaymentId);
        }

        /// <summary>
        /// complete already initiated payment
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompletePaymentAsync(int id)
        {
            var result = await _paymentService.CompletePaymentAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            return Ok(result.Message);
        }

        /// <summary>
        /// /// cancel already initiated payment
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelPayment(int id)
        {
            var result = await _paymentService.CancelPaymentAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut("{id}/retry")]
        public async Task<IActionResult> RetryPayment(int id)
        {
            var result = await _paymentService.RetryPaymentAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
    }

}
