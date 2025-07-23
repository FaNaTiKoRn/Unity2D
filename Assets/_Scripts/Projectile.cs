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
            // Buscar el componente AsteroidInfo para saber cuántos puntos vale
            AsteroidInfo info = other.GetComponent<AsteroidInfo>();
            if (info != null)
            {
                ScoreManager.Instance?.AddPoints(info.points);
                Debug.Log("🎯 Sumó " + info.points + " puntos por " + other.name);
            }
            else
            {
                Debug.LogWarning("⚠️ El asteroide no tiene AsteroidInfo: " + other.name);
            }

            other.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }


    private void XOnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Asteroid"))
        {
            string cleanName = other.gameObject.name.Replace("(Clone)", "").Trim();

            int puntos = 0;

            if (cleanName.Contains("Pequeño"))
                puntos = 1;
            else if (cleanName.Contains("Mediano"))
                puntos = 2;
            else if (cleanName.Contains("Grande"))
                puntos = 3;
            else if (cleanName.Contains("Gigante"))
                puntos = 5;

            Debug.Log($"☄️ Proyectil impactó: {cleanName} | Puntos: {puntos}");

            // Verificamos que ScoreManager exista
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.AddPoints(puntos);
            }
            else
            {
                Debug.LogWarning("❌ ScoreManager.Instance es NULL. ¿Está en escena?");
            }

            other.gameObject.SetActive(false); // Desactiva asteroide
            gameObject.SetActive(false);       // Desactiva proyectil
        }
    }
}
