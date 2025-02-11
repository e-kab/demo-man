using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed = 5;
    Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;


    public KeyCode inputLeft = KeyCode.LeftArrow;
    public KeyCode inputRight = KeyCode.RightArrow;
    public KeyCode inputUp = KeyCode.UpArrow;
    public KeyCode inputDown = KeyCode.DownArrow;
    public KeyCode placeBomb = KeyCode.LeftShift;

    public float framesPerSecond = 5;

    public GameObject bombPrefab;

    public Sprite spriteUp;
    public Sprite spriteDown;
    public Sprite spriteSide;

    // Walking animation frames
    public Sprite[] upFrames;
    public Sprite[] downFrames;
    public Sprite[] sideFrames;

    private Vector2 lastDirection = Vector2.down;


    void HandleMovement()
    {
        float inputX = 0;
        float inputY = 0;

        if (Input.GetKey(inputUp))
        {
            inputY = 1;
            spriteRenderer.sprite = spriteUp;
            lastDirection = Vector2.up;

        }
        if (Input.GetKey(inputDown))
        {
            inputY = -1;
            spriteRenderer.sprite = spriteDown;
            lastDirection = Vector2.down;


        }
        if (Input.GetKey(inputLeft))
        {
            inputX = -1;
            spriteRenderer.sprite = spriteSide;
            spriteRenderer.flipX = true;
            lastDirection = Vector2.left;

        }
        if (Input.GetKey(inputRight))
        {
            inputX = 1;
            spriteRenderer.sprite = spriteSide;
            spriteRenderer.flipX = false;
            lastDirection = Vector2.right;

        }

        Vector2 direction = new Vector2(inputX, inputY);
        if (direction.magnitude > 1)
        {
            direction.Normalize();
        }
        rb2d.linearVelocity = direction * speed;
    }

    Sprite GetAnimationFrame(Vector2 direction)
    {
        Sprite[] frames;
        if (direction == Vector2.up)
        {
            frames = upFrames;

        }
        else if (direction == Vector2.down)
        {
            frames = downFrames;

        }
        else if (direction == Vector2.left)
        {
            frames = sideFrames;
            spriteRenderer.flipX = false;
        }
  
        else
        {
            frames = sideFrames;
            spriteRenderer.flipX = true;


        }

        int index = (int)(Time.time * framesPerSecond) % frames.Length;
        return frames[index];
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void HandlePlaceBomb()
    {
        if (Input.GetKeyDown(placeBomb)) 
        {
            Vector3 bombPosition = transform.position;
            bombPosition = new Vector3(Mathf.Round(bombPosition.x), Mathf.Round(bombPosition.y), 0f);
            Instantiate(bombPrefab, bombPosition, Quaternion.identity);
        }
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();  
        HandlePlaceBomb();
    }
}
