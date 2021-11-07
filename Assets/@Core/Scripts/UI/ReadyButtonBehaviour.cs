using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ReadyButtonBehaviour : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI buttonText;

    private Toggle toggle;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }
    public void ToggleButtonText()
    {
        if (toggle.isOn)
        {
            buttonText.text = "Ready";
        } else
        {
            buttonText.text = "Not ready";
        }
    }
}
