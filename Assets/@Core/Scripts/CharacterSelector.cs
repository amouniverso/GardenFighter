using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelector : MonoBehaviour
{
    [SerializeField] private GameObject[] characters;
    [SerializeField] private int tableId;

    private int selectedCharacter = 0;

    public int getSelectedCharacter()
    {
        return selectedCharacter;
    }

    private void UpdateConnectedPlayersCharacterId()
    {
        PlayersManager.ConnectedPlayer connectedPlayer = PlayersManager.connectedPlayers[tableId];
        connectedPlayer.selectedCharacter = selectedCharacter;
        PlayersManager.connectedPlayers[tableId] = connectedPlayer;
    }
    public void NextCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter++;
        if (selectedCharacter >= characters.Length)
        {
            selectedCharacter = 0;
        }
        characters[selectedCharacter].SetActive(true);
        UpdateConnectedPlayersCharacterId();
    }

    public void PreviousCharacter()
    {
        characters[selectedCharacter].SetActive(false);
        selectedCharacter--;
        if (selectedCharacter < 0)
        {
            selectedCharacter = characters.Length - 1;
        }
        characters[selectedCharacter].SetActive(true);
        UpdateConnectedPlayersCharacterId();
    }
}
