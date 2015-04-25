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

    public abstract class BaseRepository<T> where T : new()
    {
        private readonly static object _dbRealConnectionsLocker = new object();
        private Dictionary<string, SQLiteConnectionWithLock> _dbRealConnections = new Dictionary<string, SQLiteConnectionWithLock>();
        private const string DB_NAME = "expenses.db";
        private readonly SQLiteAsyncConnection _asyncConnection;
		private bool _initialized;

        public BaseRepository()
        {
            _asyncConnection = new SQLiteAsyncConnection(GetRealDbConnection);
        }

        private SQLiteConnectionWithLock GetRealDbConnection()
        {
            var dbService = Mvx.Resolve<IDbService>();
            var folderService = Mvx.Resolve<ISpecialFolderService>();

            lock (_dbRealConnectionsLocker)
            {
                if (!_dbRealConnections.ContainsKey(DB_NAME))
                {
                    Mvx.Trace("Create connection to SQLite database " + DB_NAME);

                    var databasePath = System.IO.Path.Combine(folderService.PersonalFolderPath, DB_NAME);

                    _dbRealConnections.Add(DB_NAME, new SQLiteConnectionWithLock(dbService.GetSQLitePlatform(), new SQLiteConnectionString(databasePath, storeDateTimeAsTicks: true)));
                }
                else
                {
                    Mvx.Trace("Reuse existing connection to SQLite database " + DB_NAME);
                }
                return _dbRealConnections[DB_NAME];
            }
        }

        protected async Task InitializeDb()
		{
			if (_initialized)
				return;
			
			if (!await TableExistsAsync(typeof(T).Name))
				await Connection.CreateTableAsync<T>();
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

