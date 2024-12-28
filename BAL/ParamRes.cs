namespace BAL;

public class GetAccountsRes
{
    public List<Account> AccountList { get; set; } = [];
}

public class GetAccountRes
{
    public Account Account { get; set; } = new ();
}

public class CreateAccountRes
{
    public Account Account { get; set; } = new();
}

public class UpdateAccountRes
{
    public Account Account { get; set; } = new();
}

public class DeleteAccountRes
{
    public Account Account { get; set; } = new();
}

public class DepositRes
{
    public Transaction Transaction { get; set; } = new();
}

public class WithdrawRes
{
    public Transaction Transaction { get; set; } = new();
}

public class GetAccountBalanceRes
{
    public decimal Balance { get; set; }
}