using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] charactersPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CinemachineVirtualCamera cinemachineCam;

    void Start()
    {
        int selectedCharacter = PlayerPrefs.GetInt("selectedCharacter");
        GameObject player1 = Instantiate(charactersPrefabs[selectedCharacter], spawnPoint.position, Quaternion.Euler(0, 180, 0));
        GameObject player2 = Instantiate(charactersPrefabs[selectedCharacter - 1], spawnPoint.position, Quaternion.Euler(0, 180, 0));
        //cinemachineCam.Follow = player.transform;
        //cinemachineCam.LookAt = player.transform;
    }
}
