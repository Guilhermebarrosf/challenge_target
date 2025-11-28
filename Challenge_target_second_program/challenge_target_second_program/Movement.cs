public class Movement
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Description { get; set; } = string.Empty;
    public int ProductCode { get; set; }
    public int Quantity { get; set; }
    public DateTime MovementDate { get; set; } = DateTime.Now;
}