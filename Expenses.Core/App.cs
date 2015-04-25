using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;
using System.Linq;

namespace Depenses.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
			CreatableTypes().EndingWith("Service")
				.Union(CreatableTypes().EndingWith("Repository"))
				.AsInterfaces()
				.RegisterAsLazySingleton();

            RegisterAppStart<ViewModels.ExpenseViewModel>();
        }
    }
}