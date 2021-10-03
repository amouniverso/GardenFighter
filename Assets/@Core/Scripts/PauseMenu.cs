using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject menu;

    private bool gamePaused = false;
    private void Start()
    {
        pauseMenu.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !gamePaused && !menu.activeSelf)
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);
            gamePaused = true;
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            gamePaused = false;
        }
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
