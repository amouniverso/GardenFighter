using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject menu;
    public void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadNetLobby()
    {
        SceneManager.LoadScene("NetLobby");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        menu.SetActive(false);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        menu.SetActive(false);
    }
}
