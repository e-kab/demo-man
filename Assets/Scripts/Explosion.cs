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

        // Right Hit Detection
        Vector3 origin = transform.position;
        Vector3 rightBlast = transform.position + (new Vector3(2,0,0));
        Vector3 rightDirection = (rightBlast - origin).normalized;
        float rightDistance = Vector3.Distance(origin, rightBlast);

        RaycastHit2D rightHit = Physics2D.Raycast(origin, rightDirection, rightDistance);


        // Left Hit Detection
        Vector3 leftBlast = transform.position - (new Vector3(2, 0, 0));
        Vector3 leftDirection = (leftBlast - origin).normalized;
        float leftDistance = Vector3.Distance(origin, leftBlast);

        RaycastHit2D leftHit = Physics2D.Raycast(origin, leftDirection, leftDistance);

        if (rightHit.collider != null)
        {
            Debug.Log("Right Side Hit");
            /*
            if (rightHit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Hit");
            }
            */
        }

        if (leftHit.collider != null)
        {
            Debug.Log("Left Side Hit");
            /*
            if (rightHit.collider.GetComponent<PlayerController>() != null)
            {
                Debug.Log("Hit");
            }
            */
        }
    }
}
