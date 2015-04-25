using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Depenses.Core;
using System.Linq;

namespace Expenses.Core.Repositories
{
    public interface IExpenseRepository
    {
        Task<List<Expense>> DepensesMatching(int month);

        Task Insert(Expense d);

        Task Delete(Expense d);
    }

    public class ExpenseRepository : BaseRepository<Expense>, IExpenseRepository
    {
        public ExpenseRepository()
            : base()
        {
        }

        public async Task<List<Expense>> DepensesMatching(int month)
        {
			await InitializeDb().ConfigureAwait(false);
            var deps = await Connection.Table<Expense>().ToListAsync().ConfigureAwait(false);

            return deps.Where(d => d.ExpenseDate.Month == month)
                .OrderBy(d => d.ExpenseDate)
                .ToList();
        }

        public async Task Insert(Expense d)
        {
			await InitializeDb().ConfigureAwait(false);
            await Connection.InsertAsync(d).ConfigureAwait(false);
        }

        public async Task Delete(Expense d)
        {
			await InitializeDb().ConfigureAwait(false);
            await Connection.DeleteAsync(d).ConfigureAwait(false);
        }
    }
}

