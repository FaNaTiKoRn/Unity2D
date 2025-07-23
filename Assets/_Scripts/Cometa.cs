using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cometa : MonoBehaviour
{
    private float speed = 2f;
    private float lowerLimitY;

    private void Start()
    {
        float camBottom = Camera.main.transform.position.y - Camera.main.orthographicSize;
        lowerLimitY = camBottom - 1f;
    }

    private void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        if (transform.position.y < lowerLimitY)
            gameObject.SetActive(false);
    }

    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("💥 Colisión con nave");
            gameObject.SetActive(false);
        }
    }
}
