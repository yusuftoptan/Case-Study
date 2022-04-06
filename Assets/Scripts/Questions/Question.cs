using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Question : MonoBehaviour
{
    public GameObject questionCanvas;
    public enum options { option1, option2 };
    public options correctAnswer;

    private OptionManager[] _options;
    private OptionManager _correctAnswer;
    private int _betOnCorrectAnswer;
    private int _totalBet;

    private void Awake()
    {
        _options = GetComponentsInChildren<OptionManager>();
    }


    //Called by answer trigger
    public void ShowAnswer()
    {
        questionCanvas.SetActive(false);

        switch (correctAnswer)
        {
            case options.option1:
                _correctAnswer = _options[0];
                break;
            default:
                _correctAnswer = _options[1];
                break;
        }

        _betOnCorrectAnswer = _correctAnswer.totalBet;
        _totalBet = _options[0].totalBet + _options[1].totalBet;

        if (_betOnCorrectAnswer >= _totalBet * .5f) // .5 is min success rate to congrats player
        {
            CorrectAnswer();
        }
        else
            WrongAnswer();

        _correctAnswer.GiveMoneyBackToPlayer(); //Give money back to player from correct answer even if didn't reach to success rate
    }

    private void CorrectAnswer()
    {
        ResultTextManager.instance.ShowResultText(true);
        ParticleSystemManager.instance.Play();
        PlayerAnimationController.instance.EarnMoney();
        Bank.instance.GiveMoneyToPlayer(_betOnCorrectAnswer); //Give money to player from bank according to bet on correct answer
    }

    private void WrongAnswer()
    {
        ResultTextManager.instance.ShowResultText(false);
        PlayerAnimationController.instance.LoseMoney();
    }
}
