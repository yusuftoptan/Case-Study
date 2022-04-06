using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OptionManager : MonoBehaviour
{
    public Transform firstPositionToPlaceMoney;
    public Vector2 gapsBetweenPlacedMoney;

    private List<Transform> _moneyList;

    private void Awake()
    {
        _moneyList = new List<Transform>();
    }

    //Returns false if player can't bet money more according to the option's capacity (21)
    public bool BetMoney(Transform money)
    {
        if (_moneyList.Count >= 21)
            return false;

        _moneyList.Add(money);

        //Determine a position to place money and place it
        Vector3 position = firstPositionToPlaceMoney.position;
        position.x += gapsBetweenPlacedMoney.x * (float)((_moneyList.Count - 1) % 3);
        position.z -= gapsBetweenPlacedMoney.y * (float)((_moneyList.Count - 1) / 3);
        StartCoroutine(PlaceMoney(money, position));
        return true;
    }

    private IEnumerator PlaceMoney(Transform money, Vector3 targetPos)
    {
        Vector3 startingPos = money.position;
        float t = 0;

        while (t < 1)
        {
            t += Time.deltaTime * 4;
            money.position = Vector3.Lerp(startingPos, targetPos, t);
            yield return null;
        }
    }

    //Returns total money bet in this option
    //Called by Questions to calculate earning
    public int totalBet
    {
        get { return _moneyList.Count; }
    }

    //Called by question if this option is the correct answer
    public void GiveMoneyBackToPlayer()
    {
        for(int i = 0; i < _moneyList.Count; i++)
        {
            StackingManager.instance.StackObject(_moneyList[i]);
        }
        _moneyList.Clear();
    }
}
