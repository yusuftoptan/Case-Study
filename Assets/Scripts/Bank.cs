using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bank : MonoBehaviour
{
    public static Bank instance;

    public GameObject moneyPrefab;

    private List<GameObject> _moneyInBank;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            this.enabled = false;

        _moneyInBank = new List<GameObject>();

        //Pooling
        for (int i = 0; i < 200; i++)
        {
            GameObject money = Instantiate(moneyPrefab, transform);
            money.layer = LayerMask.NameToLayer("Stacked");
            money.transform.localScale *= .5f;
            money.SetActive(false);
            _moneyInBank.Add(money);
        }
    }

    //Called by questions to give player money from bet on correct answers
    public void GiveMoneyToPlayer(int moneyCount)
    {
        moneyCount = Mathf.Clamp(moneyCount, 0, _moneyInBank.Count); // moneyCount shouldn't be higher than the money in the bank or negative

        for (int i = 0; i < moneyCount; i++)
        {
            _moneyInBank[0].SetActive(true);
            StackingManager.instance.StackObject(_moneyInBank[0].transform);
            _moneyInBank.RemoveAt(0);
        }
    }
}
