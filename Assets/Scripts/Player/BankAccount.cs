using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BankAccount : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private Animator _moneyTextAnim;
    private int _money = 0;

    private void Start()
    {
        _moneyTextAnim = moneyText.GetComponent<Animator>();
    }

    public void EarnMoney()
    {
        _money += 100;
        moneyText.text = _money.ToString() + "$";
        _moneyTextAnim.Play("Earn Money Animation");
    }

    public void SpendMoney()
    {
        _money -= 100;
        moneyText.text = _money.ToString() + "$";
    }
}
