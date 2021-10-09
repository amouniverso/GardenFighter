using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    public void hideMainMenu()
    {
        gameObject.SetActive(false);
        backgroundMusic.Play();
    }
}
