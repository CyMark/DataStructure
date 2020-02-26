using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestDataStrucruture
{
    class TestData : IEquatable<TestData>
    {
        public string Name { get; set; }
        public int? IntValue { get; set; }
        public decimal? DecValue { get; set; }

        public bool Equals(TestData other)
        {
            return (Name == other.Name) && (IntValue == other.IntValue) && (DecValue == other.DecValue);
        }
    }
}
