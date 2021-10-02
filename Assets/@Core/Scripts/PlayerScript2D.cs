using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript2D : PlayerScript
{
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
            Rigidbody b = Instantiate(bullet, transform.Find("BulletStartPosition").position, transform.rotation);
            b.velocity = transform.right * BULLET_SPEED;
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
