using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthRender : MonoBehaviour
{
    private Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        healthText = GetComponent<Text>();
    }

    public void setHealthText(string name, string text)
    {
        string _name = name == "FIRST" ? "Player1" : "Player2";
        healthText.text = _name + ": " + text;
    }
}
