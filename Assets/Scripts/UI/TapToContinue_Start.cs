using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TapToContinue_Start : MonoBehaviour
{
    public enum types { TapToContinue, TapToStart};

    public types panelType;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Tap();
        }
    }

    private void Tap()
    {
        switch (panelType)
        {
            case types.TapToContinue:
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reset Level. Reloading level is easier and takes less time in this particular case
                break;
            case types.TapToStart:
                //Do nothing for now
                break;
        }
        gameObject.SetActive(false);
    }
}
