using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private float parallaxSpeed = 2f; // Igual a velocidad base de asteroides
    private Vector3 startPosition;
    private float height;

    private void Start()
    {
        startPosition = transform.position;
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    private void Update()
    {
        float newY = Mathf.Repeat(Time.time * parallaxSpeed, height);
        transform.position = startPosition + Vector3.down * newY;
    }
}
