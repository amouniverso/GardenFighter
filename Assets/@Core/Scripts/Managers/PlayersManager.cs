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
        InputUser.onUnpairedDeviceUsed += SpawnPlayerModel;
    }

    void SpawnPlayerModel(InputControl control, InputEventPtr eventPtr)
    {
        // Ignore anything but button presses.
        if (!(control is ButtonControl) || (control.device is Mouse)) return;

        int playerCardNumber = 0;
        foreach (GameObject table in turnTables)
        {
            if (!table.activeSelf)
            {
                table.SetActive(true);

                ConnectedPlayer connectedPlayer = new ConnectedPlayer();
                connectedPlayer.playerControl = control;
                connectedPlayers.Add(connectedPlayer);

                PlayerCard playerCard = playerCards[playerCardNumber].GetComponent<PlayerCard>();
                playerCard.GetBackgroundTextComp().SetActive(false);
                playerCard.GetInputControlTypeText().text = control.device.displayName;
                break;
            }
            playerCardNumber++;
        }

        Debug.Log("Button pressed.Control: " + control);

        //Bind the device to a player
        inputUsers.Add(InputUser.PerformPairingWithDevice(control.device));
    }

    private void OnDestroy()
    {
        foreach (InputUser user in inputUsers)
        {
            user.UnpairDevicesAndRemoveUser();
        }
        InputUser.onUnpairedDeviceUsed -= SpawnPlayerModel;
    }

}
