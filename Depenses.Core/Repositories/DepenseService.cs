using System;
using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net.Interop;

namespace Depenses.Core
{
    public interface IDepenseService
    {
        Task<List<Depense>> DepensesMatching(int month);
        Task Insert(Depense d);
        Task Delete(Depense d);
    }

    public class DepenseService : IDepenseService
    {
        private readonly SQLiteAsyncConnection _connection;

        public DepenseService(ISQLitePlatform sqlitePlatform)
        {
            _connection = new SQLiteAsyncConnection(()=> new SQLiteConnectionWithLock(sqlitePlatform, new SQLiteConnectionString("depenses.db", false)));
            _connection.CreateTableAsync<Depense>().ConfigureAwait(false);
        }

        public async Task<List<Depense>> DepensesMatching(int month)
        {
            var deps = await _connection.Table<Depense>().ToListAsync().ConfigureAwait(false);

            return deps.Where(d => d.DepenseDate.Month == month)
                .OrderBy(d => d.DepenseDate)
                .ToList();
        }

        public async Task Insert(Depense d)
        {
            await _connection.InsertAsync(d).ConfigureAwait(false);
        }

        public async Task Delete(Depense d)
        {
            await _connection.DeleteAsync(d).ConfigureAwait(false);
        }        
    }
}

