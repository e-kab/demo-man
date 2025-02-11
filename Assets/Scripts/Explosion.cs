using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class Explosion : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond = 5;
    [Tooltip("Explosion radius in units")]
    public int explosionRadius = 3;
    [Tooltip("Vertical blast prefab to instantiate")]
    public GameObject verticalBlast;

    [Tooltip("Horizontal blast prefab to instantiate")]
    public GameObject horizontalBlast;


    SpriteRenderer spriteRenderer;
    int currentFrameIndex = 0;
    float frameTimer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frameTimer = (1f / framesPerSecond);
        currentFrameIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        frameTimer -= Time.deltaTime;

        if (frameTimer <= 0)
        {
            currentFrameIndex++;
            if (currentFrameIndex >= frames.Length)
            {
                Explode();
                Destroy(gameObject);
                return;
            }
            frameTimer = (1f / framesPerSecond);
            spriteRenderer.sprite = frames[currentFrameIndex];
        }
    }

    void Explode()
    {
        Vector3 origin = transform.position;

        // Spawn explosion effect in each direction until an obstacle is hit or the radius is reached.
        SpawnBlast(origin, Vector3.right, explosionRadius);  // Right
        SpawnBlast(origin, Vector3.left, explosionRadius);   // Left
        SpawnBlast(origin, Vector3.up, explosionRadius);     // Up
        SpawnBlast(origin, Vector3.down, explosionRadius);   // Down
    }

    void SpawnBlast(Vector3 startPosition, Vector3 direction, int maxDistance)
    {
        float stepSize = 1f;  // Assuming grid-based movement with 1-unit spacing
        for (float i = stepSize; i <= maxDistance; i += stepSize)
        {
            Vector3 blastPosition = startPosition + direction * i;
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, i);

            if (hit.collider != null)
            {
                Debug.Log($"Hit {hit.collider.name} at {hit.point}. Stopping blast in this direction.");

                // Handle Player Hit
                PlayerController player = hit.collider.GetComponent<PlayerController>();
                if (player != null)
                {
                    player.ReloadScene();
                }

                // Handle Box Hit
                Box box = hit.collider.GetComponent<Box>();
                if (box != null)
                {
                    box.Break();
                }
                break;  // Stop further instantiation in this direction if an obstacle is hit.
            }

            // Instantiate the appropriate blast effect (horizontal for left/right, vertical for up/down)
            GameObject blastInstance = Instantiate(
                direction == Vector3.up || direction == Vector3.down ? verticalBlast : horizontalBlast,
                blastPosition,
                Quaternion.identity
            );

            blastInstance.transform.parent = transform; // Set as child of the explosion
        }
    }


}
