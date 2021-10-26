using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUISceneSelector : MonoBehaviour
{
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 500, 500));
        if (GUILayout.Button("RiverCastle", GUILayout.Width(100), GUILayout.Height(100)))
        {
            SceneManager.LoadScene("RiverCastle");
        }
        if (GUILayout.Button("LakeHouse", GUILayout.Width(100), GUILayout.Height(100)))
        {
            SceneManager.LoadScene("LakeHouse");
        }
        GUILayout.EndArea();
    }
}
