using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRender : MonoBehaviour
{
    [SerializeField] private PlayerScript player;
    private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        string playerName = player.playerNumber == PlayerScript.PlayerNumber.FIRST ? "Player1" : "Player2";
        healthText.text = playerName + ": " + player.health;
    }
}
