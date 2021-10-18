using Dapper;
using DapperExtensions;
using DapperExtensions.Predicate;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CityTraveler.Repository
{
    public class DbRequestManager<T> : IDbRequestManager<T> where T : class
    {
        private readonly string _connectionString;

        public DbRequestManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> SendRequestAsync(string query, object param, bool isStoredProcedure, int timeout)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = query;
                return await db.ExecuteAsync(sqlQuery, param, null, timeout, isStoredProcedure 
                    ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public async Task<IEnumerable<T>> SendQueryAsync(string query, object param, bool isStoredProcedure, int timeout)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = query;
                return await db.QueryAsync<T>(sqlQuery, param, null, timeout, isStoredProcedure
                    ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public async Task<T> ExecuteScalarAsync(string query, object param, bool isStoredProcedure, int timeout)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = query;
                return await db.ExecuteScalarAsync<T>(sqlQuery, param, null, timeout, isStoredProcedure
                    ? CommandType.StoredProcedure : CommandType.Text);
            }
        }

        public async Task<IEnumerable<object>> CommonQueryAsync(string query, object param, bool isStoredProcedure, int timeout)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                var sqlQuery = query;
                return await db.QueryAsync<object>(sqlQuery, param, null, timeout, isStoredProcedure
                    ? CommandType.StoredProcedure : CommandType.Text);
            }
        }
    }
}
