using System;
using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net.Interop;

namespace Depenses.Core
{
    public interface IExpenseService
    {
        Task<List<Expense>> DepensesMatching(int month);
        Task Insert(Expense d);
        Task Delete(Expense d);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly SQLiteAsyncConnection _connection;

        public ExpenseService(ISQLitePlatform sqlitePlatform)
        {
            _connection = new SQLiteAsyncConnection(()=> new SQLiteConnectionWithLock(sqlitePlatform, new SQLiteConnectionString("depenses.db", false)));
            _connection.CreateTableAsync<Expense>().ConfigureAwait(false);
        }

        public async Task<List<Expense>> DepensesMatching(int month)
        {
            var deps = await _connection.Table<Expense>().ToListAsync().ConfigureAwait(false);

            return deps.Where(d => d.ExpenseDate.Month == month)
                .OrderBy(d => d.ExpenseDate)
                .ToList();
        }

        public async Task Insert(Expense d)
        {
            await _connection.InsertAsync(d).ConfigureAwait(false);
        }

        public async Task Delete(Expense d)
        {
            await _connection.DeleteAsync(d).ConfigureAwait(false);
        }        
    }
}

