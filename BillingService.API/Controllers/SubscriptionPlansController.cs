using BillingService.API.Data;
using BillingService.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BillingService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class SubscriptionPlansController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SubscriptionPlansController(AppDbContext context)
        {
            _context = context;
        }

        // CREATE PLAN (Admin)
        [HttpPost]
        public async Task<IActionResult> CreatePlan(SubscriptionPlan plan)
        {
            _context.SubscriptionPlans.Add(plan);
            await _context.SaveChangesAsync();
            return Ok(plan);
        }

        // GET ALL PLANS (Public)
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPlans()
        {
            var plans = await _context.SubscriptionPlans.ToListAsync();
            return Ok(plans);
        }
    }
}
