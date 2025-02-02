using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void Break()
    {
        Destroy(gameObject);
    }
}
