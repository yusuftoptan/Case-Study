using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultTextManager : MonoBehaviour
{
    public static ResultTextManager instance;

    public TextMeshProUGUI resultText;
    public Color[] resultTextColors;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;
    }

    public void ShowResultText(bool correct)
    {
        if (correct)
        {
            resultText.text = "CORRECT!";
            resultText.color = resultTextColors[0];
        }
        else
        {
            resultText.text = "WRONG!";
            resultText.color = resultTextColors[1];
        }

        resultText.gameObject.SetActive(true);
    }
}
