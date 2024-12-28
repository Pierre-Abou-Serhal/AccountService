using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;

namespace DAL;

public partial class Dal
    {
        private readonly String dbConnectionString;
        
        public Dal(String dbConnectionString) => this.dbConnectionString = dbConnectionString;

        public async Task<List<T>> executeSqlQueryMultiRows<T>(String query, DynamicParameters? parameters, CommandType commandType, QueryType queryType)
        {
            IEnumerable<T> resultList = new List<T>();

            using (IDbConnection dbConnection = new SqlConnection(dbConnectionString))
            {

                if (dbConnection.State == ConnectionState.Broken || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                if (queryType == QueryType.SELECT)
                {
                    resultList = await dbConnection.QueryAsync<T>(sql: query, param: parameters, commandType: commandType);
                }
                else
                {
                    using (IDbTransaction dbTransaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            resultList = await dbConnection.QueryAsync<T>(sql: query, param: parameters, commandType: commandType, transaction: dbTransaction);
                            dbTransaction.Commit();
                        }
                        catch(Exception ex)
                        {
                            dbTransaction.Rollback();
                            throw new Exception($"An error occured while executing {query}. Message: {ex.Message}");
                        }
                    }
                }
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return resultList.ToList();
        }

        public async Task<T?> executeSqlQuerySingleRow<T>(String query, DynamicParameters? parameters, CommandType commandType, QueryType queryType)
        {
            T? result = default;

            using (IDbConnection dbConnection = new SqlConnection(dbConnectionString))
            {

                if (dbConnection.State == ConnectionState.Broken || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                if (queryType == QueryType.SELECT)
                {
                    result = await dbConnection.QueryFirstOrDefaultAsync<T>(sql: query, param: parameters, commandType: commandType);
                }
                else
                {
                    using (IDbTransaction dbTransaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            result = await dbConnection.QueryFirstOrDefaultAsync<T>(sql: query, param: parameters, commandType: commandType, transaction: dbTransaction);
                            dbTransaction.Commit();
                        }
                        catch(Exception ex)
                        {
                            dbTransaction.Rollback();
                            throw new Exception($"An error occured while executing {query}. Message: {ex.Message}");
                        }
                    }
                }
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
            return result;
        }

        public async Task executeSqlQueryNoReturn(String query, DynamicParameters? parameters, CommandType commandType, QueryType queryType)
        {
            using (IDbConnection dbConnection = new SqlConnection(dbConnectionString))
            {

                if (dbConnection.State == ConnectionState.Broken || dbConnection.State == ConnectionState.Closed)
                {
                    dbConnection.Open();
                }

                if (queryType == QueryType.SELECT)
                {
                    _ = await dbConnection.QueryAsync(sql: query, param: parameters, commandType: commandType);
                }
                else
                {
                    using (IDbTransaction dbTransaction = dbConnection.BeginTransaction())
                    {
                        try
                        {
                            _ = await dbConnection.QueryAsync(sql: query, param: parameters, commandType: commandType, transaction: dbTransaction);
                            dbTransaction.Commit();
                        }
                        catch(Exception ex)
                        {
                            dbTransaction.Rollback();
                            throw new Exception($"An error occured while executing {query}. Message: {ex.Message}");
                        }
                    }
                }
                if (dbConnection.State == ConnectionState.Open)
                {
                    dbConnection.Close();
                }
            }
        }

        public enum QueryType
        {
            SELECT = 0,
            UPDATE = 1,
            DELETE = 2
        }
    }