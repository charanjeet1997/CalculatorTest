using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace CalculatorSystem
{
    public class CalculatorScreen : MonoBehaviour
    {
        [SerializeField] private TMP_Text expressionText;
        [SerializeField] private TMP_Text resultText;
        
        [SerializeField] private UnityEvent<string> evaluateExpressionEvent;
        public void OnNumberButtonPressed(string number)
        {
            expressionText.text += number;
        }
        
        public void OnOperatorButtonPressed(string operatorValue)
        {
            expressionText.text += operatorValue;
        }
        
        public void OnClearButtonPressed()
        {
            expressionText.text = "";
        }
        
        public void OnResetButtonPressed()
        {
            expressionText.text = "";
            resultText.text = "";
        }
        
        public void OnBackspaceButtonPressed()
        {
            if (expressionText.text.Length > 0)
            {
                expressionText.text = expressionText.text.Substring(0, expressionText.text.Length - 1);
            }
        }
        
        public void OnEvaluateButtonPressed()
        {
            Debug.Log("Evaluate button pressed with expression: " + expressionText.text);
            evaluateExpressionEvent.Invoke(expressionText.text);
        }
        
        public void DisplayResult(string result)
        {
            resultText.text = result;
        }
    }
}