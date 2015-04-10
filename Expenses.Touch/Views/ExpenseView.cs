using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Touch.Views;
using CoreGraphics;
using Foundation;
using ObjCRuntime;
using UIKit;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.FluentLayouts.Touch;

namespace Depenses.Touch.Views
{
    [Register("ExpenseView")]
    public class ExpenseView : MvxViewController
    {
        private UITableView _tableView;

        public override void ViewDidLoad()
        {
            View = new UIView { BackgroundColor = UIColor.LightGray };
            base.ViewDidLoad();

            _tableView = new UITableView();
            _tableView.EstimatedRowHeight = 50;
            _tableView.RowHeight = UITableView.AutomaticDimension;

            var source = new MvxSimpleTableViewSource(_tableView, typeof(ExpenseCell), ExpenseCell.Identifier);
            _tableView.Source = source;

            View.AddSubviews(_tableView);

            AddLayoutConstraints();

            var set = this.CreateBindingSet<ExpenseView, Core.ViewModels.ExpenseViewModel>();
            set.Bind(source).For(p => p.ItemsSource).To(vm => vm.Depenses);
            set.Apply();

            _tableView.ReloadData();
        }

        void AddLayoutConstraints()
        {
            View.SubviewsDoNotTranslateAutoresizingMaskIntoConstraints();

            View.AddConstraints(
                _tableView.AtTopOf(View, 44),
                _tableView.AtLeftOf(View),
                _tableView.AtRightOf(View),
                _tableView.AtBottomOf(View)
            );
        }
    }
}