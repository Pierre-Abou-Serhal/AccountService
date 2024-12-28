using BAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AccountService.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountServiceController : ControllerBase
{
    private readonly AccountServiceBAL _accountServiceBal;
    private readonly AppSetting _appSetting;

    public AccountServiceController(AccountServiceBAL accountServiceBal, IOptions<AppSetting> appSettings)
    {
        _accountServiceBal = accountServiceBal;
        _appSetting = appSettings.Value;
    }
    
    [HttpGet]
    [Route("GetAccounts")]
    public async Task<GetAccountsRes> GetAccounts()
    {
        return await _accountServiceBal.GetAllAccounts();
    }

    [HttpGet]
    [Route("GetAccountById/{id:int}")]
    public async Task<GetAccountRes> GetAccount(int id)
    {
        GetAccountRes account = await _accountServiceBal.GetAccountById(id);
        return account;
    }

    [HttpPost]
    [Route("CreateAccount")]
    public async Task<CreateAccountRes> CreateAccount(CreateAccountReq req)
    {
        CreateAccountRes account = await _accountServiceBal.CreateAccount(req);
        return account;
    }

    [HttpPost]
    [Route("UpdateAccount")]
    public async Task<UpdateAccountRes> UpdateAccount(UpdateAccountReq req)
    {
        UpdateAccountRes result = await _accountServiceBal.UpdateAccount(req);
        return result;
    }

    [HttpDelete]
    [Route("DeleteAccount/{id:int}")]
    public async Task<DeleteAccountRes> DeleteAccount(int id)
    {
        DeleteAccountRes result = await _accountServiceBal.DeleteAccount(id);
        return result;
    }

    [HttpPost]
    [Route("Deposit")]
    public async Task<DepositRes> Deposit(DepositReq req)
    {
        DepositRes result = await _accountServiceBal.Deposit(req);
        return result;
    }

    [HttpPost]
    [Route("Withdraw")]
    public async Task<WithdrawRes> Withdraw(WithdrawReq req)
    {
        WithdrawRes result = await _accountServiceBal.Withdraw(req);
        return result;
    }
    
    [HttpGet]
    [Route("GetAccountBalance/{id:int}")]
    public async Task<GetAccountBalanceRes> Withdraw(int id)
    {
        GetAccountBalanceRes result = await _accountServiceBal.GetAccountBalance(id);
        return result;
    }
}