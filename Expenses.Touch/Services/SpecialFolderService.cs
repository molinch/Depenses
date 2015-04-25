using System;
using Expenses.Core;
using Expenses.Core.Repositories;

namespace Expenses.Touch
{
    public class SpecialFolderService : ISpecialFolderService
    {
        public string PersonalFolderPath
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            }
        }
    }
}

