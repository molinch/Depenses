using Cirrious.MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;
using Depenses.Core.Services;

namespace Depenses.Core.ViewModels
{
    public class ExpenseViewModel : MvxViewModel
    {
        private readonly IExpenseService _expenseService;

        public ExpenseViewModel(IExpenseService expenseService)
        {
            _expenseService = expenseService;

            _currentDepense = new Expense();
            LoadData();
        }

        private async void LoadData()
        {
            foreach (var c in GetDeps())
                await _expenseService.Insert(c).ConfigureAwait(false);
            
            var deps = await _expenseService.DepensesMatching(DateTime.Now.AddDays(-10).Month).ConfigureAwait(false);
            _depenses = new ObservableCollection<Expense>(deps);
        }
		
        private Expense _currentDepense;
        public Expense CurrentDepense
        { 
            get { return _currentDepense; }
            set
            {
                _currentDepense = value;
                RaisePropertyChanged(() => CurrentDepense);
            }
        }

        private ObservableCollection<Expense> _depenses;
        public ObservableCollection<Expense> Depenses
        { 
            get { return _depenses; }
            set
            {
                _depenses = value;
                RaisePropertyChanged(() => Depenses);
            }
        }

        public List<Expense> GetDeps()
        {
            return new List<Expense>(new []
                {
                    new Expense{ ManExpense = 123, ExpenseDate = DateTime.Today.AddDays(-12), Details = "Courses Leclerc"},
                    new Expense{ ManExpense = 45.56, ExpenseDate = DateTime.Today.AddDays(-6) },
                    new Expense{ ManExpense = 3.50, ExpenseDate = DateTime.Today.AddDays(-15), Details = "SFR Box"},
                });
        }
    }
}
