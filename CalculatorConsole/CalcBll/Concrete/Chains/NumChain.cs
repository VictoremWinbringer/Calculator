﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CalcBll.Abstract;
using CalcBll.Concrete.Expressions;

namespace CalcBll.Concrete.Chains
{
    public class NumChain : IExpressionChain
    {
        private readonly IExpressionChain _next;

        public NumChain(IExpressionChain next)
        {
            _next = next;
        }
        public void Add(ref IExpression root, string exp)
        {
            if (Regex.IsMatch(exp, @"^\d+\.?\d*$"))
            {
                var value = double.Parse(exp, CultureInfo.CreateSpecificCulture("en-US"));

                var expression = new NumberExpression(value);

                if (root == null)
                {
                    root = expression;
                }
                else
                {
                    var _root = root;

                    while (_root.Right != null)
                        _root = _root.Right;

                    if (_root is NumberExpression)
                        throw new ArgumentException("Не валидное выражение!");

                    if (_root is DivisionExpression
                        && value < 0.000001
                        && value > -0.000001)
                        throw new DivideByZeroException("Деление на ноль!");

                    _root.Right = expression;
                }
            }
            else
            {
                _next.Add(ref root, exp);
            }
        }
    }
}
