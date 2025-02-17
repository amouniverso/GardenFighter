using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.InputSystem.Users;

public class Player : MonoBehaviour, PlayerInputActions.IPlayerActions
{
    [SerializeField] Rigidbody bullet;
    [SerializeField] public int health;
    [SerializeField] public PlayerNumber playerNumber;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject headHeaderTextComp;
    [SerializeField] private int playerId;

    bool jumpKeyPressed;
    bool fireKeyPressed;

    private bool groundedPlayer;
    private Vector3 playerVelocity;
    private Vector3 playerMoveInput;
    private Vector3 playerLookDirection;

    private const float PLAYER_SPEED = 4.0f;
    private const float BULLET_SPEED = 5.0f;
    private const float JUMP_HEIGHT = 1.0f;
    private const float GRAVITY_VALUE = -9.81f;

    private CharacterController chController;
    private Camera mainCamera;
    private TextMeshProUGUI headHeaderText;

    PlayerInputActions controls;

    void Awake()
    {
        chController = GetComponent<CharacterController>();
        deathSound = GetComponent<AudioSource>();
        headHeaderText = headHeaderTextComp.GetComponent<TextMeshProUGUI>();
        mainCamera = Camera.main;
    }

    public void OnEnable()
    {
        if (controls == null)
        {
            controls = new PlayerInputActions();
            // Tell the "gameplay" action map that we want to get told about
            // when actions get triggered.
            controls.Player.SetCallbacks(this);
        }
        controls.Player.Enable();
        InputUser user;
        foreach (PlayersManager.ConnectedPlayer player in PlayersManager.connectedPlayers)
        {
            if (player.selectedCharacter == playerId)
            {
                user = InputUser.PerformPairingWithDevice(player.playerControl.device);
                user.AssociateActionsWithUser(controls);
                break;
            }
        }
        //user.ActivateControlScheme("Player");
    }

    public TextMeshProUGUI GetHeadHeaderTextComp()
    {
        return headHeaderText;
    }
    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Debug.Log("Fire!");
        fireKeyPressed = true;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started) return;
        Debug.Log("Move!");
        Vector2 moveInput = context.ReadValue<Vector2>();
        playerMoveInput = mainCamera.transform.forward * moveInput.y + mainCamera.transform.right * moveInput.x;
        playerMoveInput.y = 0f;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.started) return;
        Debug.Log("Look!");
        Vector2 lookInput = context.ReadValue<Vector2>();
        playerLookDirection = mainCamera.transform.forward * lookInput.y + mainCamera.transform.right * lookInput.x;
        playerLookDirection.y = 0f;
    }

    public void OnJump(InputAction.CallbackContext context)
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

    void playerMoveHandler()
    {
        groundedPlayer = chController.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        chController.Move(playerMoveInput * Time.deltaTime * PLAYER_SPEED);

        //Player look direction
        if (playerLookDirection != Vector3.zero)
        {
            gameObject.transform.right = playerLookDirection;
        }

        if (jumpKeyPressed && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(JUMP_HEIGHT * -3.0f * GRAVITY_VALUE);
            jumpSound.Play();
        }

        playerVelocity.y += GRAVITY_VALUE * Time.deltaTime;
        chController.Move(playerVelocity * Time.deltaTime); // y-axis move
    }

    void playerDeathHandler()
    {
        if (health <= 0)
        {
            Debug.Log("Killed");
            health = 0;
            AudioSource.PlayClipAtPoint(deathSound.clip, transform.position, 1.0f);
            Vector2 randomPosition = Random.insideUnitCircle * 2;
            chController.enabled = false;
            transform.position = new Vector3(randomPosition.x, 7, randomPosition.y);
            chController.enabled = true;
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
    }

}
