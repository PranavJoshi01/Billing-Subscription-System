using BillingService.API.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BillingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class InvoicesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public InvoicesController(AppDbContext context)
        {
            _context = context;
        }

        // USER: View own invoices
        [HttpGet("my")]
        public async Task<IActionResult> MyInvoices()
        {
            var userId = int.Parse(
                User.FindFirst(ClaimTypes.NameIdentifier)!.Value
            );

            var invoices = await _context.Invoices
                .Where(i => _context.Subscriptions
                    .Any(s => s.Id == i.SubscriptionId && s.UserId == userId))
                .ToListAsync();

            return Ok(invoices);
        }

        // ADMIN: View all invoices
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllInvoices()
        {
            return Ok(await _context.Invoices.ToListAsync());
        }
    }
}
