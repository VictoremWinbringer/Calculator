using System;
using System.Diagnostics;
using CalcBll.Abstract;
using CalcBll.Concrete;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcBll.IntegrationTest
{
    class MyWriter : IAdapter
    {
        public void Write(string message)
        {
            Debug.Write(message);
        }
    }
    [TestClass]
    public class CalcTest
    {
        private ICalc _calc;
        private double _epsilon;
        [TestInitialize]
        public void Start()
        {
            _calc = new Calc(new Parser(new ExpressionBuilder()), new Logger(new MyWriter()));
            _epsilon = 0.1d;
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Calculate_WhiteSpace()
        {
            var d = _calc.Calculate("");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Calculate_VrongOrderNotFirstNumber()
        {
            var d = _calc.Calculate("*1");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Calculate_VrongOrderNotNumberBitweenOperator()
        {
            var d = _calc.Calculate("1++1");
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Calculate_VrongOrderNotLastNumber()
        {
            var d = _calc.Calculate("1+1+");
        }

        [TestMethod]
        public void Calculate_Add()
        {
            var d = _calc.Calculate("1+2");

            Assert.IsTrue(Math.Abs(3 - d) < _epsilon);
        }

        [TestMethod]
        public void Calculate_Sub()
        {
            var d = _calc.Calculate("1-2");

            Assert.IsTrue(Math.Abs(1 - Math.Abs(d)) < _epsilon);
        }

        [TestMethod]
        public void Calculate_Mul()
        {
            var d = _calc.Calculate("1*2");

            Assert.IsTrue(Math.Abs(2 - Math.Abs(d)) < _epsilon);
        }

        [TestMethod]
        public void Calculate_Div()
        {
            var d = _calc.Calculate("1/2");

            Assert.IsTrue(Math.Abs(0.5 - Math.Abs(d)) < _epsilon);
        }

        [TestMethod]
        public void Calculate_All()
        {
            var d = _calc.Calculate("1+2-3*4/5 -0.6");

            Assert.IsTrue(Math.Abs(d) < _epsilon);

        }
    }
}
