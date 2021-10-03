using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;
using UnityEngine;

namespace HelloWorld
{
    public class NetworkPlayer : NetworkBehaviour
    {
        public override void NetworkStart()
        {
            SetHealth();
            if (!NetworkManager.Singleton.IsServer && IsOwner)
            {
                SetMouseSprite();
                SetPlayerSpriteServerRpc();
            }
        }

        [ServerRpc]
        void SetPlayerSpriteServerRpc()
        {
            SetMouseSprite();
        }

        void SetHealth()
        {
            PlayerScript player = GetComponent<PlayerScript>();
            Debug.Log(player.playerNumber.ToString());
            string healthRendererName;
            if (player.playerNumber == PlayerScript.PlayerNumber.FIRST)
            {
                healthRendererName = "PlayerHealth1";
            }
            else
            {
                healthRendererName = "PlayerHealth2";
            }
            GameObject healthRenderer = GameObject.Find(healthRendererName);
            healthRenderer.GetComponent<HealthRender>().setHealthText(player.playerNumber.ToString(), player.netHealth.Value.ToString());
        }

        private void SetMouseSprite()
        {
            PlayerScript2D playerScript2D = GetComponent<PlayerScript2D>();
            playerScript2D.playerNumber = PlayerScript.PlayerNumber.SECOND;
            Sprite mouseSprite = Resources.Load<Sprite>("Sprites/mouse");
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = mouseSprite;
            spriteRenderer.flipX = true;
            SetHealth();
        }

        void Update()
        {

        }
    }
}