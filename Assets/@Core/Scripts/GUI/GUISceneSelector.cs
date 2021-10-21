using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUISceneSelector : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        if (GUILayout.Button("ForestScene"))
        {
            SceneManager.LoadScene("ForestScene");
        }
        if (GUILayout.Button("LakehouseScene"))
        {
            SceneManager.LoadScene("Lakehouse");
        }
        if (GUILayout.Button("ProBuilderScene"))
        {
            SceneManager.LoadScene("ProBuilderDemo");
        }
        GUILayout.EndArea();
    }
}
