using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    private float upperLimitY;

    private void Start()
    {
        upperLimitY = Camera.main.transform.position.y + Camera.main.orthographicSize + 1f;
    }

    private void OnEnable()
    {
        ResetProjectile();
    }

    private void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);

        if (transform.position.y > upperLimitY)
        {
            gameObject.SetActive(false);
        }
    }

    public void ResetProjectile()
    {
        // Reiniciá aquí cualquier estado necesario cuando se active el proyectil
        // Por ejemplo, poner la posición inicial o resetear variables
        // Si usás pooling, podés dejar vacío o reiniciar lo que necesites.
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Asteroid") || other.CompareTag("Comet"))
        {
            gameObject.SetActive(false);
            // Aquí podés poner daño o efectos al enemigo
        }
    }
}
