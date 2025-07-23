using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private float speed = 2f;  // Velocidad, editable desde inspector

    private float lowerLimitY;

    private void Start()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            // Limite un poco fuera de pantalla para evitar parpadeos
            lowerLimitY = cam.transform.position.y - cam.orthographicSize - 1.5f;
        }
        else
        {
            lowerLimitY = -10f; // fallback si no hay cámara
        }
    }

    private void Update()
    {
        // Mover hacia abajo con velocidad ajustable
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Desactivar cuando salga del límite inferior
        if (transform.position.y < lowerLimitY)
        {
            gameObject.SetActive(false);
        }
    }

    // Permite ajustar la velocidad desde fuera (ej: spawner)
    public void SetSpeed(float newSpeed)
    {
        speed = newSpeed;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("💥 Colisión con la nave");
            gameObject.SetActive(false);
            // Aquí puedes poner lógica extra para daño o efectos
        }
    }
}
