﻿using Cirrious.CrossCore;
using Cirrious.MvvmCross.Touch.Platform;
using Cirrious.MvvmCross.ViewModels;
using Foundation;
using UIKit;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinIOS;
using Expenses.Core;
using Expenses.Touch;
using Expenses.Core.Repositories;

namespace Depenses.Touch
{
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
    {
        UIWindow _window;

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            var setup = new Setup(this, _window);
            setup.Initialize();

            var startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

//            Mvx.RegisterType<ISQLitePlatform, SQLitePlatformIOS>(); 
            Mvx.RegisterType<IDbService, DbService>();
            Mvx.RegisterType<ISpecialFolderService, SpecialFolderService>();

            _window.MakeKeyAndVisible();

            return true;
        }
    }
}
