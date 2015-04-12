﻿using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Depenses.Core;
using System.Linq;

namespace Expenses.Core
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> DepensesMatching(int month);

        Task Insert(Expense d);

        Task Delete(Expense d);
    }

    public class ExpenseRepository : BaseRepository
    {
        public ExpenseRepository()
            : base("expenses.db")
        {
        }

        public async Task<List<Expense>> DepensesMatching(int month)
        {
            var deps = await Connection.Table<Expense>().ToListAsync().ConfigureAwait(false);

            return deps.Where(d => d.ExpenseDate.Month == month)
                .OrderBy(d => d.ExpenseDate)
                .ToList();
        }

        public async Task Insert(Expense d)
        {
            await Connection.InsertAsync(d).ConfigureAwait(false);
        }

        public async Task Delete(Expense d)
        {
            await Connection.DeleteAsync(d).ConfigureAwait(false);
        }
    }
}
