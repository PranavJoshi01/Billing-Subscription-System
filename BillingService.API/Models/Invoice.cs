namespace BillingService.API.Models;

public class Invoice
{
    public int Id { get; set; }
    public int SubscriptionId { get; set; }
    public decimal Amount { get; set; }
    public DateTime GeneratedDate { get; set; }
    public string PaymentStatus { get; set; } = "Pending";
}
