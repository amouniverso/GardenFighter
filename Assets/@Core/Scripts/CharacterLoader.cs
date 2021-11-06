using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CharacterLoader : MonoBehaviour
{
    [SerializeField] private GameObject[] charactersPrefabs;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private CinemachineTargetGroup cinemachineGroup;

    void Start()
    {
        List<PlayersManager.ConnectedPlayer> connectedPlayers = PlayersManager.connectedPlayers;
        foreach (PlayersManager.ConnectedPlayer player in connectedPlayers)
        {
            int playerIndex = player.selectedCharacter;
            GameObject playerInstance = Instantiate(charactersPrefabs[playerIndex], spawnPoints[playerIndex].position, Quaternion.Euler(0, 180, 0));
            playerInstance.GetComponent<Player>().GetHeadHeaderTextComp().text = player.playerControl.device.displayName;
            cinemachineGroup.AddMember(playerInstance.transform, 1, 0);
        }
    }
}
