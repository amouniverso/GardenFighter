using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class GUIManager : NetworkBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
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
                //SubmitNewPosition();
            }

            GUILayout.EndArea();
        }

        [ServerRpc]
        void SpawnClientPlayerServerRpc(ServerRpcParams rpcParams = default)
        {
            GameObject go = Instantiate(playerPrefab, Vector3.one, Quaternion.identity);
            go.GetComponent<PlayerScript>().playerNumber = PlayerScript.PlayerNumber.SECOND;
            Sprite mouseSprite = Resources.Load<Sprite>("Sprites/mouse");
            go.GetComponent<SpriteRenderer>().sprite = mouseSprite;
            go.GetComponent<SpriteRenderer>().flipX = true;
            go.GetComponent<NetworkObject>().Spawn();
        }

        void StartButtons()
        {
            if (GUILayout.Button("Host"))
            {
                NetworkManager.Singleton.StartHost();
                SpawnClientPlayerServerRpc();
                //GameObject go = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
                //go.GetComponent<NetworkObject>().SpawnAsPlayerObject(NetworkManager.Singleton.LocalClientId);
            }
            if (GUILayout.Button("Client"))
            {
                NetworkManager.Singleton.StartClient();
                //SpawnClientPlayerServerRpc();
            }
            //if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
        }

        static void StatusLabels()
        {
            var mode = NetworkManager.Singleton.IsHost ?
                "Host" : NetworkManager.Singleton.IsServer ? "Server" : "Client";

            GUILayout.Label("Transport: " +
                NetworkManager.Singleton.NetworkConfig.NetworkTransport.GetType().Name);
            GUILayout.Label("Mode: " + mode);
        }

        static void SubmitNewPosition()
        {
            if (GUILayout.Button(NetworkManager.Singleton.IsServer ? "Move" : "Request Position Change"))
            {
                if (NetworkManager.Singleton.ConnectedClients.TryGetValue(NetworkManager.Singleton.LocalClientId,
                    out var networkedClient))
                {
                    var player = networkedClient.PlayerObject.GetComponent<NetworkPlayer>();
                    if (player)
                    {
                        player.Move();
                    }
                }
            }
        }
    }
}