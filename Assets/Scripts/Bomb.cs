using UnityEngine;

public class Bomb : MonoBehaviour
{

    public GameObject explosionPrefab;
    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond = 3;


    SpriteRenderer spriteRenderer;
    int currentFrameIndex = 0;
    float frameTimer;

    private BoxCollider2D bombCollider;
    private GameObject placingPlayer;

    public void SetPlacingPlayer(GameObject player)
    {
        placingPlayer = player;  // Store the player that placed the bomb
        // Debug.Log("Placing Player: " + placingPlayer.name); // Debugging
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        bombCollider = GetComponent<BoxCollider2D>();
        bombCollider.isTrigger = true;  // Start as trigger

        frameTimer = (1f / framesPerSecond);
        currentFrameIndex = 0;
        Invoke("Explode", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer <= 0)
        {
            currentFrameIndex++;
            frameTimer = (1f / framesPerSecond);
            spriteRenderer.sprite = frames[currentFrameIndex];
        }
    }

    void Explode()
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == placingPlayer)
        {
            // Debug.Log("Player exited bomb trigger");
            bombCollider.isTrigger = false;  // Solidify once the player leaves
        }
    }
}
