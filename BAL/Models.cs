namespace BAL;

public class Account
{
    public int Id { get; set; }
    public string AccountHolderName { get; set; } = string.Empty;
    public decimal InitialDeposit { get; set; }
    public string AccountType { get; set; } = string.Empty;
}

public class Transaction
{
    public decimal Amount { get; set; }
    public int AccountId { get; set; }
}