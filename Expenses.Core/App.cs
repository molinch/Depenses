using Cirrious.CrossCore.IoC;
using Cirrious.CrossCore;

namespace Depenses.Core
{
    public class App : Cirrious.MvvmCross.ViewModels.MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();
            
            RegisterAppStart<ViewModels.ExpenseViewModel>();
        }
    }
}