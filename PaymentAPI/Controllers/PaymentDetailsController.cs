using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PaymentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace PaymentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailsController : ControllerBase
    {
        private readonly PaymentDetailContext _context;
        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IEnumerable<PaymentDetail>> GetPaymentDetails()
        {
            return await _context.paymentDetails.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentDetail>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if (paymentDetail==null)
            {
                return NotFound();
            }
            return paymentDetail;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetail>> PostPaymentDetail(PaymentDetail paymentDetail)
        {
            _context.paymentDetails.Add(paymentDetail);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetail.PaymentDetailID, paymentDetail });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PaymentDetail>> PutPaymentDetail(int id, PaymentDetail paymentDetail)
        {
            if (id!=paymentDetail.PaymentDetailID)
            {
                return BadRequest();
            }
            _context.Entry(paymentDetail).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentDetailExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentDetail>> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.paymentDetails.FindAsync(id);
            if (paymentDetail==null)
            {
                return NotFound();
            }
            _context.paymentDetails.Remove(paymentDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        private bool PaymentDetailExists(int id)
        {
            return _context.paymentDetails.Any(e => e.PaymentDetailID == id);
        }
    }
}






