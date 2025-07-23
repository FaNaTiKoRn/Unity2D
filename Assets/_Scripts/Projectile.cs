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
        // Reiniciar estado si hace falta (vacío por ahora)
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            AsteroidInfo info = other.GetComponent<AsteroidInfo>();

            if (info != null)
            {
                // Sumar puntos
                ScoreManager.Instance?.AddPoints(info.points);
                Debug.Log("🎯 Sumó " + info.points + " puntos por " + other.name);

                // Reproducir sonido de impacto asignado al prefab
                SoundManager.Instance.PlaySound(info.impactSound);
            }
            else
            {
                Debug.LogWarning("⚠️ El asteroide no tiene AsteroidInfo: " + other.name);
            }

            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }
}
