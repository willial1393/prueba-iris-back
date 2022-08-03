namespace Core.Entities;

public interface IChat
{
    public string Timestamp { get; set; }
    public int TotalContactClients { get; set; }
    public int ReasonClaim { get; set; }
    public int WarrantyReason { get; set; }
    public int ReasonDoubt { get; set; }
    public int ReasonPurchase { get; set; }
    public int ReasonCongratulations { get; set; }
    public int ReasonChange { get; set; }
    public string Hash { get; set; }
}