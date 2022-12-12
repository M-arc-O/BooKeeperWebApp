namespace BooKeeperWebApp.Shared.Models;
public record AddMutationModel(
    DateTime Date,
    string AccountNumber, 
    string OtherAccountNumber, 
    string Description,
    string? Comment,
    string? Tag,
    double Amount,
    double AmountAfterMutation,
    Guid AccountId,
    Guid BookId,
    Guid? EventId);
