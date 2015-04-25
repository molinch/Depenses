using System;
using Depenses.Core;
using System.Threading.Tasks;
using System.Collections.Generic;
using SQLite.Net;
using SQLite.Net.Interop;
using Cirrious.CrossCore;
using SQLite.Net.Async;
using System.Linq;

namespace Expenses.Core.Repositories
{
    public interface IDbService
    {
        ISQLitePlatform GetSQLitePlatform();
    }

    public interface ISpecialFolderService
    {
        string PersonalFolderPath { get; }
    }

    public abstract class BaseRepository
    {
        private readonly static object _dbRealConnectionsLocker = new object();
        private Dictionary<string, SQLiteConnectionWithLock> _dbRealConnections = new Dictionary<string, SQLiteConnectionWithLock>();
        private readonly string _databaseName;
        private readonly SQLiteAsyncConnection _asyncConnection;

        public BaseRepository(string dbName)
        {
            _databaseName = dbName;
            _asyncConnection = new SQLiteAsyncConnection(GetRealDbConnection);
            InitializeDb();
        }

        private SQLiteConnectionWithLock GetRealDbConnection()
        {
            var dbService = Mvx.Resolve<IDbService>();
            var folderService = Mvx.Resolve<ISpecialFolderService>();

            lock (_dbRealConnectionsLocker)
            {
                if (!_dbRealConnections.ContainsKey(_databaseName))
                {
                    Mvx.Trace("Create connection to SQLite database " + _databaseName);

                    var databasePath = System.IO.Path.Combine(folderService.PersonalFolderPath, _databaseName);

                    _dbRealConnections.Add(_databaseName, new SQLiteConnectionWithLock(dbService.GetSQLitePlatform(), new SQLiteConnectionString(databasePath, storeDateTimeAsTicks: true)));
                }
                else
                {
                    Mvx.Trace("Reuse existing connection to SQLite database " + _databaseName);
                }
                return _dbRealConnections[_databaseName];
            }
        }

        private async void InitializeDb()
        {
            if (!await TableExistsAsync("Expense"))
                await Connection.CreateTableAsync<Expense>();
        }

        protected SQLiteAsyncConnection Connection
        {
            get { return _asyncConnection; }
        }

        private async Task<bool> TableExistsAsync(string tableName)
        {
            var count = await Connection.ExecuteScalarAsync<int>("SELECT count(*) FROM sqlite_master WHERE type='table' AND name=?", tableName).ConfigureAwait(false);
            return count == 1;
        }
    }
}

