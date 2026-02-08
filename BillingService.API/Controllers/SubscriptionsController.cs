using BillingService.API.Data;
using BillingService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BillingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SubscriptionsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubscriptionsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("{planId}")]
        public async Task<IActionResult> Subscribe(int planId)
        {
            var userId = int.Parse(
                User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)!.Value
            );

            var plan = await _context.SubscriptionPlans.FindAsync(planId);
            if (plan == null)
                return NotFound("Plan not found");

            var subscription = new Subscription
            {
                UserId = userId,
                PlanId = plan.Id,
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddDays(plan.DurationInDays),
                Status = "Active"
            };

            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();

            // AUTO CREATE INVOICE
            var invoice = new Invoice
            {
                SubscriptionId = subscription.Id,
                Amount = plan.Price,
                GeneratedDate = DateTime.UtcNow,
                PaymentStatus = "Pending"
            };

            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                subscription,
                invoice
            });
        }

    }
}
