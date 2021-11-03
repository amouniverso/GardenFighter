using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] charactersPrefabs;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private CinemachineTargetGroup cinemachineGroup;

    void Start()
    {
        //Debug.Log(PlayersManager.connectedPlayers[0].selectedCharacter);
        //Debug.Log(PlayersManager.connectedPlayers[0].playerControl.device.displayName);

        //Debug.Log(PlayersManager.connectedPlayers[1].selectedCharacter);
        //Debug.Log(PlayersManager.connectedPlayers[1].playerControl.device.displayName);

        foreach (GameObject prefab in charactersPrefabs)
        {
           // GameObject player = Instantiate(prefab, spawnPoint.position, Quaternion.Euler(0, 180, 0));
            //PlayerInput playerInput = player.GetComponent<PlayerInput>();
            //playerInput.
        }
        GameObject player1 = Instantiate(charactersPrefabs[0], spawnPoint.position, Quaternion.Euler(0, 180, 0));
        GameObject player2 = Instantiate(charactersPrefabs[1], spawnPoint.position, Quaternion.Euler(0, 180, 0));
        cinemachineGroup.AddMember(player1.transform, 1, 0);
        cinemachineGroup.AddMember(player2.transform, 1, 0);
    }
}
