using System;
using System.Diagnostics;
using CalcBll.Abstract;
using CalcBll.Concrete;
using CalcBll.Concrete.Chains;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CalcBll.Tests
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
            _calc = new Calc(new Parser(new ExpressionBuilder(
                new NumChain(
                    new AddChain(
                        new SubChain(
                            new MulChain(
                                new DivChain(
                                    null)))))), new ExpressionValidator())
                , new Logger(new MyWriter()));
            _epsilon = 0.1d;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_Alpha()
        {
             _calc.Calculate("1a+1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Calculate_WhiteSpace()
        {
            _calc.Calculate("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_VrongOrderNotFirstNumber()
        {
            _calc.Calculate("*1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_VrongOrderNotNumberBitweenOperator()
        {
             _calc.Calculate("1++1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Calculate_VrongOrderNotLastNumber()
        {
             _calc.Calculate("1+1+");
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

        [TestMethod]
        public void Calculate_All_86_2()
        {
            var d = _calc.Calculate("23 * 2 + 45 - 24 / 5");

            Assert.IsTrue(Math.Abs(Math.Abs(d) -86.2) < _epsilon);

        }
    }
}
