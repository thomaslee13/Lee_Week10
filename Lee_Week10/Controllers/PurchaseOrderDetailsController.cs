#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lee_Week10.Data;
using Lee_Week10.Models;

namespace Lee_Week10.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseOrderDetailsController : ControllerBase
    {
        private readonly Adventureworks2019Context _context;

        public PurchaseOrderDetailsController(Adventureworks2019Context context)
        {
            _context = context;
        }

        // GET: api/PurchaseOrderDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseOrderDetail>>> GetPurchaseOrderDetails()
        {
            return await _context.PurchaseOrderDetails.ToListAsync();
        }

        // GET: api/PurchaseOrderDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseOrderDetail>> GetPurchaseOrderDetail(int id)
        {
            var purchaseOrderDetail = await _context.PurchaseOrderDetails.FindAsync(id);

            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }

            return purchaseOrderDetail;
        }

        // PUT: api/PurchaseOrderDetails/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchaseOrderDetail(int id, PurchaseOrderDetail purchaseOrderDetail)
        {
            if (id != purchaseOrderDetail.PurchaseOrderId)
            {
                return BadRequest();
            }

            _context.Entry(purchaseOrderDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PurchaseOrderDetailExists(id))
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

        // POST: api/PurchaseOrderDetails
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PurchaseOrderDetail>> PostPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail)
        {
            _context.PurchaseOrderDetails.Add(purchaseOrderDetail);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PurchaseOrderDetailExists(purchaseOrderDetail.PurchaseOrderId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPurchaseOrderDetail", new { id = purchaseOrderDetail.PurchaseOrderId }, purchaseOrderDetail);
        }

        // DELETE: api/PurchaseOrderDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchaseOrderDetail(int id)
        {
            var purchaseOrderDetail = await _context.PurchaseOrderDetails.FindAsync(id);
            if (purchaseOrderDetail == null)
            {
                return NotFound();
            }

            _context.PurchaseOrderDetails.Remove(purchaseOrderDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PurchaseOrderDetailExists(int id)
        {
            return _context.PurchaseOrderDetails.Any(e => e.PurchaseOrderId == id);
        }
    }
}
