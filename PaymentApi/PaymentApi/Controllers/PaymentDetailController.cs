using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentApi.Models;

namespace PaymentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailController(PaymentDetailContext context)
        {
            _context = context; 
        }
        //GET : api/PaymentDetail
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetail>>> GetPaymentDetails()
        {
            if(_context.PaymentDetails == null)
            {
                return NotFound();
            }
            return await _context.PaymentDetails.ToListAsync();
        }
        //GET : api/PaymentDetail/5 
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            return await _context.PaymentDetails.FindAsync(id);
        }
        //DELETE : api/PaymentDetail/5
        [HttpDelete("{id}")]

        public async Task<ActionResult> DeletePaymentDetail(int id)
        {
            try
            {
                var paymentdetail = await _context.PaymentDetails.FindAsync(id);
                if (paymentdetail == null)
                {
                    return NotFound();
                }
                _context.PaymentDetails.Remove(paymentdetail);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal server error");
            }
        }
        //POST : Add a new payment record
        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            if (_context.PaymentDetails == null)
            {
                return Problem("PaymentDetails is null");
            }
            _context.PaymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailId },paymentDetail);
        } 

    }
}
