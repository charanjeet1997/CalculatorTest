using UnityEngine;
using UnityEngine.Events;

namespace CalculatorSystem
{
    public class CalculatorController : MonoBehaviour
    {

        private string currentExpression = "";
        private ICalculator calculator;
        
        public UnityEvent<string> OnExpressionEvaluated;

        private void Start()
        {
            calculator = new Calculator();
        }

        public void StartEvaluation(string value)
        {
            Debug.Log($"Evaluation Started: {value}");
            Evaluate(value);
        }
        

        private void Evaluate(string value)
        {
            currentExpression = value;
            string result = calculator.EvaluateExpression(currentExpression);
            OnExpressionEvaluated.Invoke(result);
            Debug.Log($"Expression: {currentExpression}, Result: {result}");
        }
        
    }
}