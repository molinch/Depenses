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
    public class DepenseCell : MvxTableViewCell
    {
        public static NSString Identifier = new NSString("DepenseCell");

        private UILabel _sophAmount;
        private UILabel _benAmount;
        private UILabel _details;
        private UILabel _date;

        public DepenseCell(IntPtr handle)
            : base(handle)
        {
            CreateControls();
            CreateBindings();
            AddLayoutConstraints();
        }

        void CreateControls()
        {
            _sophAmount = new UILabel
            {
//                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.FromRGB(243, 123, 176),
            };

            _benAmount = new UILabel
            {
//                Font = UIFont.SystemFontOfSize(12),
                TextColor = UIColor.FromRGB(0, 133, 202)
            };
            _details = new UILabel { Lines = 0 };
            _date = new UILabel();

            ContentView.AddSubviews(_sophAmount, _benAmount, _details, _date);
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
                var set = this.CreateBindingSet<DepenseCell, Depense>();
                set.Bind(_sophAmount).For(p => p.Text).To(vm => vm.Sophie);
                set.Bind(_benAmount).For(p => p.Text).To(vm => vm.Ben);
                set.Bind(this).For(p => p.DepenseDate).To(vm => vm.DepenseDate);
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

                _benAmount.ToRightOf(_date, margin),
                _benAmount.WithSameTop(_date),
                _benAmount.WithSameBottom(_date),
                _benAmount.WithSameWidth(ContentView).WithMultiplier(1/6f),

                _sophAmount.ToRightOf(_benAmount, margin),
                _sophAmount.WithSameTop(_benAmount),
                _sophAmount.WithSameBottom(_date),
                _sophAmount.WithSameWidth(ContentView).WithMultiplier(1/6f),

                _details.ToRightOf(_sophAmount, margin),
                _details.WithSameTop(_sophAmount),
                _details.AtBottomOf(ContentView, margin),
                _details.AtRightOf(ContentView, margin)
            );
        }
    }
}

