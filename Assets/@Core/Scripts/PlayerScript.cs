using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] protected Transform groundCheckTransform;
    [SerializeField] protected LayerMask playerMask;
    [SerializeField] protected Rigidbody bullet;
    [SerializeField] public int health;
    [SerializeField] public PlayerNumber playerNumber;

    protected bool jumpKeyPressed;
    protected bool fireKeyPressed;
    protected float horizontalInput;
    protected float verticalInput;

    private bool jumpKey;
    private bool fireKey;
    private bool leftKeyDown, leftKeyUp, 
                 rightKeyDown, rightKeyUp,
                 upKeyDown, upKeyUp,
                 downKeyDown, downKeyUp;

    protected const int VELOCITY = 4;
    protected const int ROTATION = 4;
    protected const int JUMP_POWER = 8;
    protected const int BULLET_SPEED = 5;

    protected AudioSource deathSound;
    protected Rigidbody rgdbody;

    private GameObject healthRenderer;

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

    //IEnumerator zeroHealth()
    //{


    void Update()
    {
        if (health <= 0)
        {
            health = 0;
            AudioSource.PlayClipAtPoint(deathSound.clip, transform.position);
            Vector2 randomPosition = Random.insideUnitCircle * 2;
            gameObject.transform.position = new Vector3(randomPosition.x, 7, randomPosition.y);
            health = 1;
        }
        if (playerNumber == PlayerNumber.FIRST)
        {
            jumpKey = Input.GetKeyDown(KeyCode.LeftControl);
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
        rgdbody.velocity = new Vector3(transform.right.x * verticalInput * VELOCITY, rgdbody.velocity.y, transform.right.z * verticalInput * VELOCITY);
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
