using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCard : MonoBehaviour
{
    [SerializeField] private GameObject backgroundTextComp;
    [SerializeField] private GameObject inputControlTypeTextComp;

    private TextMeshProUGUI inputControlTypeText;

    private void OnEnable()
    {
        inputControlTypeText = inputControlTypeTextComp.GetComponent<TextMeshProUGUI>();
    }

    public TextMeshProUGUI GetInputControlTypeText()
    {
        return inputControlTypeText;
    }

    public GameObject GetBackgroundTextComp()
    {
        return backgroundTextComp;
    }

}
