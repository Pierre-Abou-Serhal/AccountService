namespace BAL;

public class CreateAccountReq
{
    public Account Account { get; set; } = new();
}

public class UpdateAccountReq
{
    public Account Account { get; set; } = new();
}

public class DepositReq
{
    public Transaction Transaction { get; set; } = new();
}

public class WithdrawReq
{
    public Transaction Transaction { get; set; } = new();
}