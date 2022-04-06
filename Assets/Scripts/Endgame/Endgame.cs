using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Endgame : MonoBehaviour
{
    public GameObject endgameVcam;
    public Transform tower;
    public Transform placementPosition;
    public GameObject tapToContinuePanel;

    private GameObject player;
    private List<Transform> _moneyList;

    public void StartEndgame()
    {
        player = StackingManager.instance.gameObject; //Get player from a singleton script
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<BankAccount>().moneyText.gameObject.SetActive(false);

        //Get all the money stacked at the end. Make them not following player anymore
        _moneyList = player.GetComponent<StackingManager>().stackedObjects;
        for(int i = 1; i < _moneyList.Count; i++) //Loop starts from 1 because first element is not money stacked but is the target for first money to follow
        {
            StackedObjectMovement obj = _moneyList[i].GetComponent<StackedObjectMovement>();
            obj.StopFollowingQueue();
        }

        endgameVcam.SetActive(true);
        StartCoroutine(LiftPlayerUp());
    }

    private IEnumerator LiftPlayerUp()
    {
        player.transform.SetParent(tower);
        Vector3 startingPos = player.transform.position;
        float t = 0;

        //Move player to endgame point
        while (t < 1)
        {
            t += Time.deltaTime * 1;
            player.transform.position = Vector3.Lerp(startingPos, placementPosition.position, t);
            yield return null;
        }

        player.GetComponent<PlayerAnimationController>().run = false;
        //Rotate player towards camera

        Vector3 startingAngles = player.transform.localEulerAngles;
        t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * 3;
            player.transform.localEulerAngles = Vector3.Lerp(startingAngles, Vector3.zero, t);
            yield return null;
        }
        PlayerAnimationController.instance.Dance();

        Vector3 towerStartingPosition;
        Vector3 towerTargetPosition;
        Vector3 moneyStartingScale;
        Vector3 moneyTargetScale;

        //Start placing money from the bottom, lift player up
        for (int i = 1; i < _moneyList.Count; i++) //Loop starts from 1 because first element is not money stacked but is the target for first money to follow
        {
            towerStartingPosition = tower.position;
            towerTargetPosition = towerStartingPosition + Vector3.up * .3f;

            startingPos = _moneyList[i].position;

            moneyStartingScale = _moneyList[i].localScale;
            moneyTargetScale = moneyStartingScale * 2f;

            t = 0;
            while (t < 1)
            {
                t += Time.deltaTime * 10;
                _moneyList[i].position = Vector3.Lerp(startingPos, placementPosition.position, t);
                _moneyList[i].localScale = Vector3.Lerp(moneyStartingScale, moneyTargetScale, t);
                tower.position = Vector3.Lerp(towerStartingPosition, towerTargetPosition, t);
                yield return null;
            }
            _moneyList[i].SetParent(tower);
        }

        PlayerAnimationController.instance.Dance();
        tapToContinuePanel.SetActive(true);
    }
}
