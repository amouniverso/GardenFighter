using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //[SerializeField] Transform groundCheckTransform;
    //[SerializeField] LayerMask playerMask;
    [SerializeField] Rigidbody bullet;
    [SerializeField] public int health;
    [SerializeField] public PlayerNumber playerNumber;

    bool jumpKeyPressed;
    bool fireKeyPressed;

    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private Vector3 playerLookInput;

    private const float PLAYER_SPEED = 4.0f;
    private const float ROTATION = 4.0f;
    private const float JUMP_POWER = 8.0f;
    private const float BULLET_SPEED = 5.0f;
    private const float JUMP_HEIGHT = 1.0f;
    private const float GRAVITY_VALUE = -9.81f;


    private AudioSource deathSound;
    //private Rigidbody rgdbody;
    //private GameObject healthRenderer;
    private CharacterController chController;

    public void Fire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log("Fire!");
        fireKeyPressed = true;
    }

    public void Move(InputAction.CallbackContext context)
    {
        if (context.started) return;
        Debug.Log("Move!");
        Vector2 moveDirection = context.ReadValue<Vector2>();
        playerMoveInput = new Vector3(-moveDirection.x, 0, -moveDirection.y);
    }

    public void Look(InputAction.CallbackContext context)
    {
        if (context.started) return;
        Debug.Log("Look!");
        Vector2 lookDirection = context.ReadValue<Vector2>();
        playerLookInput = new Vector3(-lookDirection.x, 0, -lookDirection.y);
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Jump pressed!");
            jumpKeyPressed = true;
        } else if (context.canceled)
        {
            Debug.Log("Jump released!");
            jumpKeyPressed = false;
        }       
    }

    public enum PlayerNumber
    {
        FIRST = 1,
        SECOND = 2
    }

    void Start()
    {
        chController = GetComponent<CharacterController>();
        deathSound = GetComponent<AudioSource>();
        //rgdbody = GetComponent<Rigidbody>();
    }

    void playerMoveHandler()
    {
        groundedPlayer = chController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        chController.Move(playerMoveInput * Time.deltaTime * PLAYER_SPEED);

        //Player look direction
        if (playerLookInput != Vector3.zero)
        {
            gameObject.transform.right = playerLookInput;
        }

        if (jumpKeyPressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY_VALUE);
        }

        playerVelocity.y += GRAVITY_VALUE * Time.deltaTime;
        chController.Move(playerVelocity * Time.deltaTime); // y-axis move
    }

    void playerDeathHandler()
    {
        if (health <= 0)
        {
            health = 0;
            AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
            Vector2 randomPosition = Random.insideUnitCircle * 2;
            gameObject.transform.position = new Vector3(randomPosition.x, 7, randomPosition.y);
            health = 1;
        }
    }

    void playerFireHandler()
    {
        if (fireKeyPressed)
        {
            Rigidbody b = Instantiate(bullet, transform.Find("BulletStartPosition").position, transform.rotation);
            b.velocity = transform.right * BULLET_SPEED;
            fireKeyPressed = false;
        }
    }

    void Update()
    {
        playerDeathHandler();
        playerMoveHandler();
    }

    private void FixedUpdate()
    {
        playerFireHandler();

        //if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        //{
        //    return;
        //}

        //if (jumpKeyPressed)
        //{
        //    rgdbody.AddForce(Vector3.up * JUMP_POWER, ForceMode.VelocityChange);
        //    jumpKeyPressed = false;
       // }
        
    }
}
