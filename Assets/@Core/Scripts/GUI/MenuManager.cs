using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject pauseMenu;

    private bool gamePaused = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused && !mainMenu.activeSelf)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            gamePaused = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gamePaused = false;
        }
    }
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

    public void FullScreen()
    {
        // Toggle fullscreen
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void StartHost()
    {
        NetworkManager.Singleton.StartHost();
        mainMenu.SetActive(false);
    }

    public void StartClient()
    {
        NetworkManager.Singleton.StartClient();
        mainMenu.SetActive(false);
    }

}
