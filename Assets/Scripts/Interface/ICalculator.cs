using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CalculatorSystem
{
    public interface ICalculator
    {
        string EvaluateExpression(string expression);
        string Clear();
        string Reset();
    }
}