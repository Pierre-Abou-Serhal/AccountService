namespace BAL;

public class CreateAccountReq
{
    public Account Account { get; set; } = new();
}

public class UpdateAccountReq
{
    public Account Account { get; set; } = new();
}

public class DeleteAccountReq
{
    private int Id { get; set; }
}

public class DepositReq
{
    private Transaction Transaction { get; set; } = new();
}

public class WithdrawReq
{
    private Transaction Transaction { get; set; } = new();
}