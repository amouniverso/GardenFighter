using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAPI;
using MLAPI.Messaging;
using MLAPI.NetworkVariable;

public class PlayerScript2D : PlayerScript
{

    [ServerRpc]
    void SpawnBulletServerRpc()
    {
        CreateBullet();
    }

    void CreateBullet()
    {
        Rigidbody rb = Instantiate(bullet, transform.Find("BulletStartPosition").position, transform.rotation);
        rb.velocity = transform.right * BULLET_SPEED;
        rb.gameObject.GetComponent<NetworkObject>().Spawn();
    }
    private void FixedUpdate()
    {
        rgdbody.velocity = new Vector3(
            transform.right.x * verticalInput * VELOCITY,
            rgdbody.velocity.y,
            transform.right.z * verticalInput * VELOCITY
        );
        transform.Rotate(0, horizontalInput * ROTATION, 0);

        if (fireKeyPressed)
        {
            if (NetworkManager.Singleton.IsServer)
            {
                CreateBullet();
            }
            else
            {
                SpawnBulletServerRpc();
            }
            fireKeyPressed = false;
        }

        if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }

        if (jumpKeyPressed)
        {
            rgdbody.AddForce(Vector3.up * JUMP_POWER, ForceMode.VelocityChange);
            jumpKeyPressed = false;
        }

    }
}
