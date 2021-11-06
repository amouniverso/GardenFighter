using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] Button startButton;
    [SerializeField] Button[] readyButtons;

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

}
