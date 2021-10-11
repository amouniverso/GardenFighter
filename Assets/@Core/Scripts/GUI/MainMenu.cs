using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private AudioSource backgroundMusic;
    [SerializeField] private GameObject cinemachineCam;
    public void hideMainMenu()
    {
        gameObject.SetActive(false);
        backgroundMusic.PlayDelayed(1.5f);
        cinemachineCam.SetActive(true);
    }
}
