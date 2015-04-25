using System;
using System.Collections.Generic;
using SQLite.Net;
using System.Linq;
using SQLite.Net.Async;
using System.Threading.Tasks;
using SQLite.Net.Interop;
using Expenses.Core;
using Expenses.Core.Repositories;

namespace Depenses.Core.Services
{
    public interface IExpenseService
    {
        Task<List<Expense>> DepensesMatching(int month);

        Task Insert(Expense d);

        Task Delete(Expense d);
    }

    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public async Task<List<Expense>> DepensesMatching(int month)
        {
            return await _expenseRepository.DepensesMatching(month).ConfigureAwait(false);
        }

        public async Task Insert(Expense d)
        {
            await _expenseRepository.Insert(d).ConfigureAwait(false);
        }

        public async Task Delete(Expense d)
        {
            await _expenseRepository.Delete(d).ConfigureAwait(false);
        }
    }
}

