using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Calculator : MonoBehaviour
{
    public TextMeshProUGUI sum_text;

    private int leftValue;
    private int rightValue;
    private string currentOperator;
    private bool isLeft;

    // Start is called before the first frame update
    void Start()
    {
        sum_text = GetComponent<TextMeshProUGUI>();
        sum_text.text = "0";
        leftValue = 0;
        isLeft = true;
        currentOperator = "";
    }

    

    public void GetValue(string value)
    {
        if (sum_text.text == "0" && value != "+" && value != "-" && value != "*" && value != "/")
        {
            sum_text.text = value;
        }
        else if (value == "+" || value == "-" || value == "*" || value == "/")
        {
            GetLeftValue();
            currentOperator = value;
            isLeft = false;
            sum_text.text = "0";
        }
        else if (value == "=" && !isLeft)
        {
            GetRightValue();
            switch (currentOperator)
            {
                case "+":
                    sum_text.text = (leftValue + rightValue).ToString();
                    leftValue += rightValue;
                    break;
                case "-":
                    sum_text.text = (leftValue - rightValue).ToString();
                    leftValue -= rightValue;
                    break;
                case "*":
                    sum_text.text = (leftValue * rightValue).ToString();
                    leftValue *= rightValue;
                    break;
                case "/":
                    if (rightValue != 0)
                    {
                        sum_text.text = (leftValue / rightValue).ToString();
                        leftValue /= rightValue;
                    }
                    else
                    {
                        sum_text.text = "Error";
                    }
                    break;
            }
            isLeft = true;
        }
        else if (value == "C")
        {
            ClearValue();
        }
        else
        {
            sum_text.text += value;
        }
    }

    public void GetLeftValue()
    {
        leftValue = Convert.ToInt32(sum_text.text);
    }

    public void GetRightValue()
    {
        rightValue = Convert.ToInt32(sum_text.text);
    }

    public void ClearValue()
    {
        leftValue = 0;
        rightValue = 0;
        sum_text.text = "0";
        isLeft = true;
        currentOperator = "";
    }
}
