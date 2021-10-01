using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private Rigidbody bullet;
    [SerializeField] public int health = 5;
    [SerializeField] public PlayerNumber playerNumber;
    [SerializeField] private Text gameoverText;

    private bool jumpKeyPressed;
    private bool fireKeyPressed;
    private float horizontalInput;
    private float verticalInput;

    private bool jumpKey;
    private bool fireKey;
    private bool leftKeyDown, leftKeyUp, 
                 rightKeyDown, rightKeyUp,
                 upKeyDown, upKeyUp,
                 downKeyDown, downKeyUp;

    private const int VELOCITY = 4;
    private const int ROTATION = 4;
    private const int JUMP_POWER = 8;
    private const int BULLET_SPEED = 5;

    private AudioSource deathSound;
    private Rigidbody rgdbody;

    public enum PlayerNumber
    {
        FIRST = 1,
        SECOND = 2
    }

    // Start is called before the first frame update
    void Start()
    {
        deathSound = GetComponent<AudioSource>();
        rgdbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (health == 0)
        {
            AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
            gameoverText.text = "Player " + (playerNumber == PlayerNumber.FIRST ? "2" : "1") + " WIN!";
            Destroy(gameObject);
        }
        if (playerNumber == PlayerNumber.FIRST)
        {
            jumpKey = Input.GetKeyDown(KeyCode.Space);
            fireKey = Input.GetKeyDown(KeyCode.LeftShift);

            leftKeyDown = Input.GetKeyDown(KeyCode.A);
            leftKeyUp = Input.GetKeyUp(KeyCode.A);
            rightKeyDown = Input.GetKeyDown(KeyCode.D);
            rightKeyUp = Input.GetKeyUp(KeyCode.D);

            upKeyDown = Input.GetKeyDown(KeyCode.S);
            upKeyUp = Input.GetKeyUp(KeyCode.S);
            downKeyDown = Input.GetKeyDown(KeyCode.W);
            downKeyUp = Input.GetKeyUp(KeyCode.W);
        }
        else if(playerNumber == PlayerNumber.SECOND)
        {
            jumpKey = Input.GetKeyDown(KeyCode.RightControl);
            fireKey = Input.GetKeyDown(KeyCode.RightShift);

            leftKeyDown = Input.GetKeyDown(KeyCode.LeftArrow);
            leftKeyUp = Input.GetKeyUp(KeyCode.LeftArrow);
            rightKeyDown = Input.GetKeyDown(KeyCode.RightArrow);
            rightKeyUp = Input.GetKeyUp(KeyCode.RightArrow);

            upKeyDown = Input.GetKeyDown(KeyCode.DownArrow);
            upKeyUp = Input.GetKeyUp(KeyCode.DownArrow);
            downKeyDown = Input.GetKeyDown(KeyCode.UpArrow);
            downKeyUp = Input.GetKeyUp(KeyCode.UpArrow);
        }

        if (jumpKey) jumpKeyPressed = true; 
        if (fireKey) fireKeyPressed = true;
        horizontalInput = leftKeyDown ? -1.0f : rightKeyDown ? 1.0f : (rightKeyUp || leftKeyUp) ? 0f : horizontalInput;
        verticalInput = upKeyDown ? -1.0f : downKeyDown ? 1.0f : (downKeyUp || upKeyUp) ? 0f : verticalInput;

    }

    private void FixedUpdate()
    {
        rgdbody.velocity = new Vector3(transform.forward.x * verticalInput * VELOCITY, rgdbody.velocity.y, transform.forward.z * verticalInput * VELOCITY);
        transform.Rotate(0, horizontalInput * ROTATION, 0);

        if (fireKeyPressed)
        {
            Rigidbody b = Instantiate(bullet, transform.Find("BulletStartPosition").position, transform.rotation);
            b.velocity = transform.forward * BULLET_SPEED;
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
