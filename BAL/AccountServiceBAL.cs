namespace BAL;

public class AccountServiceBAL
{
    public async Task<GetAccountsRes> GetAllAccounts()
    {
        return new GetAccountsRes();
    }

    public async Task<GetAccountRes> GetAccountById(int Id)
    {
        return new GetAccountRes();
    }

    public async Task<CreateAccountRes> CreateAccount(CreateAccountReq req)
    {
        return new CreateAccountRes();
    }

    public async Task<UpdateAccountRes> UpdateAccount(UpdateAccountReq req)
    {
        return new UpdateAccountRes();
    }

    public async Task<DeleteAccountRes> DeleteAccount(DeleteAccountReq req)
    {
        return new DeleteAccountRes();
    }

    public async Task<DepositRes> Deposit(DepositReq req)
    {
        return new DepositRes();
    }

    public async Task<WithdrawRes> Withdraw(WithdrawReq req)
    {
        return new WithdrawRes();
    }
}