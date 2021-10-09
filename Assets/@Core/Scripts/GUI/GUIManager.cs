using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class GUIManager : MonoBehaviour
    {
        void OnGUI()
        {
            GUILayout.BeginArea(new Rect(10, 10, 300, 300));
            if (!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
            {
                StartButtons();
            }
            else
            {
                StatusLabels();
                TestButton();
            }
            GUILayout.EndArea();
        }

        void StartButtons()
        {
            if (GUILayout.Button("Host"))
            {
                NetworkManager.Singleton.StartHost();
            }
            if (GUILayout.Button("Client"))
            {
                NetworkManager.Singleton.StartClient();
            }
        }

        void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
            GUILayout.Label("Client ID: " + NetworkManager.Singleton.LocalClientId);
            GUILayout.Label("IsServer: " + NetworkManager.Singleton.IsServer);
            GUILayout.Label("IsClient: " + NetworkManager.Singleton.IsClient);
            GUILayout.Label("IsHost: " + NetworkManager.Singleton.IsHost);
        }

        void TestButton()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "FromServer" : "FromClient"))
            {
                //Debug.Log("IsServer: " + NetworkManager.Singleton.IsServer);
                //TestServerRpc(NetworkManager.Singleton.IsServer);
            }
        }
    }
}