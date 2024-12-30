using System.Data;
using DAL;
using Dapper;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.CompilerServices;

namespace BAL;

public class AccountServiceBAL
{
    private readonly AppSetting _appSetting;

    public AccountServiceBAL(IOptions<AppSetting> appSettings)
    {
        _appSetting = appSettings.Value;
    }
    
    public async Task<GetAccountsRes> GetAllAccounts()
    {
        GetAccountsRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        res.AccountList = await dal.executeSqlQueryMultiRows<Account>("usp_GetAllAccounts", null, CommandType.StoredProcedure, Dal.QueryType.SELECT);
        
        res.AccountList.Add(new()
        {
            Id = 99,
            AccountType = "Test Mo",
            InitialDeposit = 50000,
            AccountHolderName = "This Account Was Added By Code And Is Not In The DataBase"
        });
        
        return res;
    }

    public async Task<GetAccountRes> GetAccountById(int id)
    {
        GetAccountRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", id);
        
        res.Account = await dal.executeSqlQuerySingleRow<Account>("usp_GetAccountById", queryParams, CommandType.StoredProcedure, Dal.QueryType.SELECT) ?? new ();
        
        return res;
    }

    public async Task<CreateAccountRes> CreateAccount(CreateAccountReq req)
    {
        CreateAccountRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", req.Account.Id);
        queryParams.Add("AccountHolderName", req.Account.AccountHolderName);
        queryParams.Add("InitialDeposit", req.Account.InitialDeposit);
        queryParams.Add("AccountType", req.Account.AccountType);
        
        res.Account = await dal.executeSqlQuerySingleRow<Account>("usp_UpdateAccount", queryParams, CommandType.StoredProcedure, Dal.QueryType.UPDATE) ?? new ();
        
        return res;
    }

    public async Task<UpdateAccountRes> UpdateAccount(UpdateAccountReq req)
    {
        UpdateAccountRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", req.Account.Id);
        queryParams.Add("AccountHolderName", req.Account.AccountHolderName);
        queryParams.Add("InitialDeposit", req.Account.InitialDeposit);
        queryParams.Add("AccountType", req.Account.AccountType);
        
        res.Account = await dal.executeSqlQuerySingleRow<Account>("usp_UpdateAccount", queryParams, CommandType.StoredProcedure, Dal.QueryType.UPDATE) ?? new ();
        
        return res;
    }

    public async Task<DeleteAccountRes> DeleteAccount(int id)
    {
        DeleteAccountRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", id);
        
        res.Account = await dal.executeSqlQuerySingleRow<Account>("usp_DeleteAccount", queryParams, CommandType.StoredProcedure, Dal.QueryType.DELETE) ?? new ();
        
        return res;
    }

    public async Task<DepositRes> Deposit(DepositReq req)
    {
        DepositRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", req.Transaction.AccountId);
        queryParams.Add("Amount", Math.Abs(req.Transaction.Amount));
        
        res.Transaction = await dal.executeSqlQuerySingleRow<Transaction>("usp_CreateTransaction", queryParams, CommandType.StoredProcedure, Dal.QueryType.UPDATE) ?? new ();
        
        return res;
    }

    public async Task<WithdrawRes> Withdraw(WithdrawReq req)
    {
        WithdrawRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", req.Transaction.AccountId);
        queryParams.Add("Amount", -Math.Abs(req.Transaction.Amount));
        
        res.Transaction = await dal.executeSqlQuerySingleRow<Transaction>("usp_CreateTransaction", queryParams, CommandType.StoredProcedure, Dal.QueryType.UPDATE) ?? new ();
        
        return res;
    }
    
    public async Task<GetAccountBalanceRes> GetAccountBalance(int id)
    {
        GetAccountBalanceRes res = new ();

        Dal dal = new(_appSetting.ConnString);

        DynamicParameters queryParams = new ();
            
        queryParams.Add("AccountId", id);
        
        res.Balance = await dal.executeSqlQuerySingleRow<Decimal>("usp_GetAccountBalance", queryParams, CommandType.StoredProcedure, Dal.QueryType.SELECT);
        
        return res;
    }
}