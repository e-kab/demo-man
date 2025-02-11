using UnityEngine;
using System.Collections;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;
using static UnityEngine.UI.Image;

public class Explosion : MonoBehaviour
{

    [Tooltip("The individual sprites of the animation")]
    public Sprite[] frames;
    [Tooltip("How fast does the animation play")]
    public float framesPerSecond = 5;
    [Tooltip("Explosion radius in units")]
    public int explosionRadius = 2;
    [Tooltip("Explosion prefab to instantiate")]
    public GameObject explosionPrefab;


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

        // Right Hit Detection
        Vector3 rightBlast = origin + new Vector3(explosionRadius, 0, 0);
        Vector3 rightDirection = (rightBlast - origin).normalized;
        float rightDistance = Vector3.Distance(origin, rightBlast);
        RaycastHit2D rightHit = Physics2D.Raycast(origin, rightDirection, rightDistance);


        // Left Hit Detection
        Vector3 leftBlast = origin - new Vector3(explosionRadius, 0, 0);
        Vector3 leftDirection = (leftBlast - origin).normalized;
        float leftDistance = Vector3.Distance(origin, leftBlast);
        RaycastHit2D leftHit = Physics2D.Raycast(origin, leftDirection, leftDistance);

        // Up Hit Detection
        Vector3 upBlast = origin + new Vector3(0, explosionRadius, 0);
        Vector3 upDirection = (upBlast - origin).normalized;
        float upDistance = Vector3.Distance(origin, upBlast);
        RaycastHit2D upHit = Physics2D.Raycast(origin, upDirection, upDistance);

        // Down Hit Detection
        Vector3 downBlast = origin - new Vector3(0, explosionRadius, 0);
        Vector3 downDirection = (downBlast - origin).normalized;
        float downDistance = Vector3.Distance(origin, downBlast);
        RaycastHit2D downHit = Physics2D.Raycast(origin, downDirection, downDistance);

        // Handle collisions
        if (rightHit.collider != null)
        {

            // Debug.Log($"Right Side Hit at {rightHit.point} on {rightHit.collider.name}");
            if (rightHit.collider.GetComponent<PlayerController>() != null)
            {
                rightHit.collider.GetComponent<PlayerController>().ReloadScene();
            }
            else if (rightHit.collider.GetComponent<Box>() != null)
            {
                rightHit.collider.GetComponent<Box>().Break();
            }
        }


        if (leftHit.collider != null)
        {
            // Debug.Log($"Left Side Hit at {leftHit.point} on {leftHit.collider.name}");
            if (leftHit.collider.GetComponent<PlayerController>() != null)
            {
                leftHit.collider.GetComponent<PlayerController>().ReloadScene();
            }
            else if (leftHit.collider.GetComponent<Box>() != null)
            {
                leftHit.collider.GetComponent<Box>().Break();
            }
        }

        if (upHit.collider != null)
        {
            // Debug.Log("Up Side Hit: " + upHit.collider.name);
            if (upHit.collider.GetComponent<PlayerController>() != null)
            {
                upHit.collider.GetComponent<PlayerController>().ReloadScene();
            }
            else if (upHit.collider.GetComponent<Box>() != null)
            {
                upHit.collider.GetComponent<Box>().Break();
            }
        }

        if (downHit.collider != null)
        {
            // Debug.Log($"Down Side Hit at {downHit.point} on {downHit.collider.name}");
            if (downHit.collider.GetComponent<PlayerController>() != null)
            {
                downHit.collider.GetComponent<PlayerController>().ReloadScene();
            }
            else if (downHit.collider.GetComponent<Box>() != null)
            {
                downHit.collider.GetComponent<Box>().Break();
            }
        }
    }

}
