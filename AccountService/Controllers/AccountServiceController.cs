using BAL;
using Microsoft.AspNetCore.Mvc;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountServiceController : ControllerBase
{
    private readonly AccountServiceBAL _accountServiceBal;

    public AccountServiceController(AccountServiceBAL accountServiceBal)
    {
        _accountServiceBal = accountServiceBal;
    }

    [HttpGet]
    public async Task<GetAccountsRes> GetAccounts()
    {
        return await _accountServiceBal.GetAllAccounts();
    }

    [HttpGet("{id}")]
    public async Task<GetAccountRes> GetAccount(int id)
    {
        var account = await _accountServiceBal.GetAccountById(id);
        
        return account;
    }

    [HttpPost]
    public async Task<CreateAccountRes> CreateAccount(CreateAccountReq req)
    {
        var account = await _accountServiceBal.CreateAccount(req);
        return account;
    }

    [HttpPost]
    public async Task<UpdateAccountRes> UpdateAccount(UpdateAccountReq req)
    {
        var result = await _accountServiceBal.UpdateAccount(req);
        return result;
    }

    [HttpDelete("{id}")]
    public async Task<DeleteAccountRes> DeleteAccount(DeleteAccountReq req)
    {
        var result = await _accountServiceBal.DeleteAccount(req);
        return result;
    }

    [HttpPost]
    public async Task<DepositRes> Deposit(DepositReq req)
    {
        var result = await _accountServiceBal.Deposit(req);
        return result;
    }

    [HttpPost]
    public async Task<WithdrawRes> Withdraw(WithdrawReq req)
    {
        var result = await _accountServiceBal.Withdraw(req);
        return result;
    }
}