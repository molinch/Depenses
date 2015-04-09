using System;
using System.Collections.Generic;
using System.Linq;
using SQLite.Net.Attributes;

namespace Depenses.Core
{
    public class Depense
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public double Sophie { get; set; }

        public double Ben { get; set; }

        public DateTime DepenseDate { get; set; }

        public string Details { get; set; }
    }
}
