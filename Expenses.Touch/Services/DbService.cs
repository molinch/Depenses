using System;
using Expenses.Core;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using Cirrious.CrossCore;

namespace Expenses.Touch
{
    public class DbService : IDbService
    {
        public ISQLitePlatform GetSQLitePlatform()
        {
            return new SQLite.Net.Platform.XamarinIOS.SQLitePlatformIOS();
        }
    }
}

