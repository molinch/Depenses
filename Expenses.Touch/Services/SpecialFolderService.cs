using System;
using Expenses.Core;

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

