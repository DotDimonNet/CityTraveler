using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace CityTraveler.Repository
{
    public interface IDbRequestManager<T> where T : class
    {
        public Task<int> SendRequestAsync(string query, object param, bool isStoredProcedure, int timeout = 36000);
        public Task<IEnumerable<T>> SendQueryAsync(string query, object param, bool isStoredProcedure, int timeout = 36000);
        public Task<T> ExecuteScalarAsync(string query, object param, bool isStoredProcedure, int timeout = 36000);
        public Task<IEnumerable<dynamic>> CommonQueryAsync(string query, object param, bool isStoredProcedure, int timeout = 36000);
    }
}
