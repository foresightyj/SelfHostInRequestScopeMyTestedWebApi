using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SelfHostWebApiNinjectInRequestScope.Models
{
    public class Calc : ICalc
    {
        private static int _currentId = 0;

        public Calc()
        {
            _currentId++;
            Id = _currentId;
        }

        public int Id { get; private set; }

        public int Add(int a, int b)
        {
            return a + b;
        }
    }
}