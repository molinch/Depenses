using System;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Foundation;
using UIKit;
using Cirrious.FluentLayouts.Touch;
using Cirrious.MvvmCross.Binding.BindingContext;
using Depenses.Core.ViewModels;
using Depenses.Core;

namespace Depenses.Touch
{
    public class ExpenseCell : MvxTableViewCell
    {
        public static NSString Identifier = new NSString("DepenseCell");

        private UILabel _womanAmount;
        private UILabel _menAmount;
        private UILabel _details;
        private UILabel _date;

        public ExpenseCell(IntPtr handle)
            : base(handle)
        {
            CreateControls();
            CreateBindings();
            AddLayoutConstraints();
        }

        void CreateControls()
        {
            _womanAmount = new UILabel
            {
//                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.FromRGB(243, 123, 176),
            };

            _menAmount = new UILabel
            {
//                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.FromRGB(0, 133, 202)
            };
            _details = new UILabel { Lines = 0 };
            _date = new UILabel();

            ContentView.AddSubviews(_womanAmount, _menAmount, _details, _date);
        }

        private DateTime _depenseDate;
        public DateTime DepenseDate
        { 
            get { return _depenseDate; }
            set
            {
                _depenseDate = value;
                _date.Text = _depenseDate.ToString("dd-MM-yyyy");
            }
        }

        void CreateBindings()
        {
            this.DelayBind(() => {
                var set = this.CreateBindingSet<ExpenseCell, Expense>();
                set.Bind(_womanAmount).For(p => p.Text).To(vm => vm.WomanExpense);
                set.Bind(_menAmount).For(p => p.Text).To(vm => vm.ManExpense);
                set.Bind(this).For(p => p.DepenseDate).To(vm => vm.ExpenseDate);
                set.Bind(_details).For(p => p.Text).To(vm => vm.Details);
                set.Apply();
            });
        }

        void AddLayoutConstraints()
        {
            ContentView.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            const int margin = 5;

            ContentView.AddConstraints(
                _date.AtLeftOf(ContentView, margin),
                _date.AtTopOf(ContentView, margin),
                _date.AtBottomOf(ContentView, margin),
                _date.WithSameWidth(ContentView).WithMultiplier(1/3f),

                _menAmount.ToRightOf(_date, margin),
                _menAmount.WithSameTop(_date),
                _menAmount.WithSameBottom(_date),
                _menAmount.WithSameWidth(ContentView).WithMultiplier(1/6f),

                _womanAmount.ToRightOf(_menAmount, margin),
                _womanAmount.WithSameTop(_menAmount),
                _womanAmount.WithSameBottom(_date),
                _womanAmount.WithSameWidth(ContentView).WithMultiplier(1/6f),

                _details.ToRightOf(_womanAmount, margin),
                _details.WithSameTop(_womanAmount),
                _details.AtBottomOf(ContentView, margin),
                _details.AtRightOf(ContentView, margin)
            );
        }
    }
}

