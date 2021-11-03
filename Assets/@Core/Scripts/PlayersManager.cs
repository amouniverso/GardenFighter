using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;

public class PlayersManager : MonoBehaviour
{
    [SerializeField] private GameObject[] turnTables;
    [SerializeField] private GameObject[] playerCards;

    private List<InputUser> inputUsers = new List<InputUser>();

    public struct ConnectedPlayer
    {
        public int selectedCharacter;
        public InputControl playerControl;

    }

    public static List<ConnectedPlayer> connectedPlayers = new List<ConnectedPlayer>();

    private void Awake()
    {
       ++InputUser.listenForUnpairedDeviceActivity;
        InputUser.onUnpairedDeviceUsed += SpawnPlayer;
    }

    void SpawnPlayer(InputControl control, InputEventPtr eventPtr)
    {
        // Ignore anything but button presses.
        if (!(control is ButtonControl)) return;

        int playerCardNumber = 0;
        foreach (GameObject table in turnTables)
        {
            if (!table.activeSelf)
            {
                table.SetActive(true);

                CharacterSelector tableCharacterSelector = table.GetComponent<CharacterSelector>();
                ConnectedPlayer connectedPlayer = new ConnectedPlayer();
                connectedPlayer.selectedCharacter = tableCharacterSelector.getSelectedCharacter();
                connectedPlayer.playerControl = control;
                connectedPlayers.Add(connectedPlayer);

                playerCards[playerCardNumber].GetComponent<PlayerCard>().backgroundText.SetActive(false);
                break;
            }
            playerCardNumber++;
        }

        Debug.Log("Button pressed.Control: " + control);
        inputUsers.Add(InputUser.PerformPairingWithDevice(control.device));

        //PlayerInput.Instantiate(playerPrefab, device: control.device);
    }

    private void OnDestroy()
    {
        foreach (InputUser user in inputUsers)
        {
            user.UnpairDevicesAndRemoveUser();
        }
        InputUser.onUnpairedDeviceUsed -= SpawnPlayer;
    }

}
