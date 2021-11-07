using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Toggle[] readyButtons;

    private bool isReady = false;
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        isReady = true;
        foreach(Toggle toggle in readyButtons)
        {
            if (!toggle.isOn)
            {
                isReady = false;
                break;
            } 
        }
        if (PlayersManager.connectedPlayers.Capacity == 0)
        {
            isReady = false;
        }
        startButton.interactable = isReady;
    }

}
