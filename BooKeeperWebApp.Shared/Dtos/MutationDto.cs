namespace BooKeeperWebApp.Shared.Dtos;
public class MutationDto : IBaseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string AccountNumber { get; set; }
    public string OtherAccountNumber { get; set; }
    public string Description { get; set; }
    public string? Comment { get; set; }
    public string? Tag { get; set; }
    public double Amount { get; set; }
    public double AmountAfterMutation { get; set; }

    public string BookName { get; set; }
    public string? EventName { get; set; }
}
