using Cirrious.MvvmCross.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Depenses.Core.ViewModels
{
    public class DepenseViewModel 
		: MvxViewModel
    {
        private readonly IDepenseService _depenseService;

        public DepenseViewModel(IDepenseService depenseService)
        {
            _depenseService = depenseService;

            _currentDepense = new Depense();
            LoadData();
        }

        private async void LoadData()
        {
//            GetDeps().Select(c =>
//                {
//                    _depenseService.Insert(c);
//                    return c;
//                }).ToList();
            
            var deps = await _depenseService.DepensesMatching(DateTime.Now.AddDays(-10).Month).ConfigureAwait(false);
            _depenses = new ObservableCollection<Depense>(deps);
        }
		
        private Depense _currentDepense;
        public Depense CurrentDepense
        { 
            get { return _currentDepense; }
            set
            {
                _currentDepense = value;
                RaisePropertyChanged(() => CurrentDepense);
            }
        }

        private ObservableCollection<Depense> _depenses;
        public ObservableCollection<Depense> Depenses
        { 
            get { return _depenses; }
            set
            {
                _depenses = value;
                RaisePropertyChanged(() => Depenses);
            }
        }

        public List<Depense> GetDeps()
        {
            return new List<Depense>(new []
                {
                    new Depense{ Ben = 123, DepenseDate = DateTime.Today.AddDays(-12), Details = "Truc de minic de merde"},
                    new Depense{ Ben = 45.56, DepenseDate = DateTime.Today.AddDays(-6) },
                    new Depense{ Ben = 3.50, DepenseDate = DateTime.Today.AddDays(-15), Details = "Truc de minic de merde, Truc de minic de merde, Truc de minic de merde"},
                });
        }
    }
}
