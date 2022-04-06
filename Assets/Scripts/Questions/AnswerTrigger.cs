using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnswerTrigger : MonoBehaviour
{
    public Question question;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            question.ShowAnswer();
    }
}
