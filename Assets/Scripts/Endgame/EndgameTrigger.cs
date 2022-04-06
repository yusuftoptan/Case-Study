using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameTrigger : MonoBehaviour
{
    public Endgame endgame;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            endgame.StartEndgame();
    }
}
