using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net.Attributes;

namespace Depenses.Core
{
    public class Expense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double WomanExpense { get; set; }

        public double ManExpense { get; set; }

        public DateTime ExpenseDate { get; set; }

        public string Details { get; set; }
    }
}
